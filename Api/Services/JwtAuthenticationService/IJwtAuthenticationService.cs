using LanguageExt;
using LanguageExt.Common;
using Shared.Models;

namespace WebAppAuthorization.Services.JwtAuthenticationService;

public interface IJwtAuthenticationService
{
    Task<Result<User>> RegisterAsync(RegisterModel registerModel);
    Task<Result<AuthModel>> GetTokenAsync(TokenRequestModel requestModel);
    Task<Result<Unit>> AddToRoleAsync(ManageRoleModel roleModel);
    Task<Result<Unit>> RemoveRoleAsync(ManageRoleModel roleModel);
    Task<IList<string>> GetUserRoles(string userName); 
}