using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using WebAppAuthorization.Constants;
using WebAppAuthorization.Extensions;
using WebAppAuthorization.Options;
using WebAppAuthorization.Persistence.Repositories.RefreshTokenRepository;
using WebAppAuthorization.Services.DateTimeProvider;

namespace WebAppAuthorization.Services.JwtAuthenticationService;

public class JwtAuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager,
    IOptions<Jwt> jwt, IDateTimeProvider dateTimeProvider, IRefreshTokenRepository tokenRepository,
    TokenValidationParameters tokenValidationParameters) : IJwtAuthenticationService
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
    private readonly Jwt _jwtConfig = jwt.Value;

    public async Task<IList<string>> GetUserRoles(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        return user is not null ?
            await userManager.GetRolesAsync(user) : 
            Array.Empty<string>();
    }

    public async Task<Result<Unit>> RemoveRoleAsync(ManageRoleModel roleModel)
    {
        var user = await userManager.FindByNameAsync(roleModel.UserName);
        return await ManageRole(user, roleModel, RemoveUserFromRole);
    }

    public async Task<Result<AuthModel>> RefreshAsync(AuthModel authModel)
    {
        var tokenHash = RefreshTokenHelpers.Hashed(authModel.RefreshToken);
        var accessTokenHash = RefreshTokenHelpers.Hashed(authModel.Token);

        var tokenEntity = await tokenRepository.GetByTokenHash(tokenHash);

        if (!IsRefreshTokenValid(tokenEntity, accessTokenHash))
            return new Result<AuthModel>(new ArgumentException("Invalid refresh token"));
            
        var userClaims = GetClaimsPrincipalFromToken(authModel.Token);

        if (userClaims is null)
            return new Result<AuthModel>(new ArgumentException("Invalid access token"));
        
        var userId = userClaims.Claims.Single(c => c.Type == "name").Value;
        var user = userManager.Users.ToList().SingleOrDefault(u => u.Id.ToString() == userId);

        if (user is null)
            return new Result<AuthModel>(new ArgumentException("Invalid user token"));

        tokenEntity!.Revoked = true;
        await tokenRepository.SaveAsync();
        
        return new Result<AuthModel>(await CreateAuthResultForUser(user));
    }

    private bool IsRefreshTokenValid(RefreshToken? refreshToken, string accessTokenHash) =>
        refreshToken is not null && !refreshToken.Revoked &&
            refreshToken.ExpirationDate > dateTimeProvider.GetCurrentTime() &&
            refreshToken.AccessTokenHash == accessTokenHash;

    private ClaimsPrincipal? GetClaimsPrincipalFromToken(string token)
    {
        var accessTokenHandler = new JwtSecurityTokenHandler();

        try { return accessTokenHandler.ValidateToken(token, tokenValidationParameters, out _); }
        catch { return null; }
    }

    public async Task<Result<Unit>> AddToRoleAsync(ManageRoleModel roleModel)
    {
        var user = await userManager.FindByNameAsync(roleModel.UserName);
        return await ManageRole(user, roleModel, AddUserToRole);
    }

    private static async Task<Result<Unit>> ManageRole(User? user, ManageRoleModel roleModel, Func<User, Role, Task> roleManager)
    {
        if (user is null)
        {
            var error = new ArgumentException($"User {roleModel.UserName} doesn't exist");
            return new Result<Unit>(error);
        }
        
        if (Enum.TryParse<Role>(roleModel.Role, out var role))
        {
            await roleManager(user, role);
            return new Result<Unit>();
        }
        var invalidRoleException = new ArgumentException($"No existing role: {roleModel.Role}");
        return new Result<Unit>(invalidRoleException);
    }

    private async Task RemoveUserFromRole(User user, Role role)
    {
        if (await userManager.IsInRoleAsync(user, role.ToString()))
        {
            await userManager.RemoveFromRoleAsync(user, role.ToString());
        }
    }

    private async Task AddUserToRole(User user, Role role)
    {
        if (!await userManager.IsInRoleAsync(user, role.ToString()))
        {
            await userManager.AddToRoleAsync(user, role.ToString());
        }
    }

    public async Task<Result<AuthModel>> GetTokenAsync(TokenRequestModel requestModel)
    {
        var user = await userManager.FindByEmailAsync(requestModel.Email);
        if (user is null)
        {
            var noAccountError = new ArgumentException($"No accounts with Email: {requestModel.Email}");
            return new Result<AuthModel>(noAccountError);
        }

        if (await userManager.CheckPasswordAsync(user, requestModel.Password))
        {
            var token = await CreateAuthResultForUser(user);
            return new Result<AuthModel>(token);
        }
        
        var invalidCredentialsError = new ArgumentException($"Invalid Credentials for user {user.UserName}");
        return new Result<AuthModel>(invalidCredentialsError);
    }

    private async Task<AuthModel> CreateAuthResultForUser(User user)
    {
        var token = await CreateJwtToken(user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = Guid.NewGuid().ToString();

        var refreshTokenEntity = RefreshTokenHelpers.NewTokenValue(refreshToken, accessToken, dateTimeProvider.GetCurrentTime().AddDays(10));
        
        tokenRepository.Add(refreshTokenEntity);
        await tokenRepository.SaveAsync();
        
        return new AuthModel { Token = accessToken,  RefreshToken = refreshToken };
    }

    private async Task<JwtSecurityToken> CreateJwtToken(User user)
    {
        var claims = await CreateClaims(user);
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        var expires = dateTimeProvider.GetCurrentTime().AddMinutes(_jwtConfig.DurationInMinutes);

        return new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials);
    }

    private async Task<IEnumerable<Claim>> CreateClaims(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(j => new Claim("roles", j));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        return claims.Union(userClaims).Union(roleClaims);
    }

    public async Task<Result<User>> RegisterAsync(RegisterModel registerModel)
    {
        if (!await IsPasswordValid(registerModel.Password))
        {
            var passError = new ArgumentException("Incorrect Password");
            return new Result<User>(passError);
        }
        
        if (await userManager.FindByEmailAsync(registerModel.Email) is not null)
        {
            var emailError = new ArgumentException("Account with such email already exists");
            return new Result<User>(emailError);
        }
        
        var user = new User
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            CreatedAt = dateTimeProvider.GetCurrentTime(),
        };
        
        var result = await userManager.CreateAsync(user, registerModel.Password);
        await AddUserToRole(user, Role.User);
        if (result.Succeeded)
        {
            return new Result<User>(user);
        }
        
        var creationError = new ArgumentException($"Couldn't create user {user.UserName}");
        return new Result<User>(creationError);
    }

    private async Task<bool> IsPasswordValid(string pass)
    {
        foreach (var validator in userManager.PasswordValidators)
        {
            var res = await validator.ValidateAsync(userManager, null, pass);
            
            if (!res.Succeeded) return false;
        }

        return true;
    }
}