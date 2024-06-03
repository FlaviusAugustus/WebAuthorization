using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using WebAppAuthorization.Constants;
using WebAppAuthorization.Services.JwtAuthenticationService;

namespace WebAppAuthorization.Controllers;

[ApiController]
[Route("api/auth")]
public class AccountController(IJwtAuthenticationService userService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
    {
        var result = await userService.RegisterAsync(registerModel);
        return result.Match<IActionResult>(
            success => Ok(success),
            fail => BadRequest(fail.Message)
        );
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(TokenRequestModel requestModel)
    {
        var result = await userService.GetTokenAsync(requestModel);
        return result.Match<IActionResult>(
            success => success is null ? NotFound() : Ok(success),
                fail => BadRequest(fail.Message)
            );
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshAsync(AuthModel authModel)
    {
        var result = await userService.RefreshAsync(authModel);
        return result.Match<IActionResult>(
            success => Ok(success),
                fail => Unauthorized(fail.Message)
        );
    }

    [HttpGet]
    [Route("get-user-roles")]
    [Authorize(Policy = nameof(Policy.CanSeeUserRoles))]
    public async Task<IActionResult> GetUserRoles(string userName)
    {
        var roles = await userService.GetUserRoles(userName);
        return Ok(roles);
    }

    [HttpPost]
    [Route("add-user-role")]
    [Authorize(Policy = nameof(Policy.CanManageRoles))] 
    public async Task<IActionResult> AddUserToRole(ManageRoleModel roleModel)
    {
        var result = await userService.AddToRoleAsync(roleModel);
        return result.Match<IActionResult>(
            success => Ok(success),
            fail => result.IsBottom ? Ok() : BadRequest(fail.Message) 
            );
    }

    [HttpDelete]
    [Route("remove-user-role")]
    [Authorize(Policy = nameof(Policy.CanManageRoles))] 
    public async Task<IActionResult> RemoveUserFromRole(ManageRoleModel roleModel)
    {
        var result = await userService.RemoveRoleAsync(roleModel);
        return result.Match<IActionResult>(
            success => Ok(),
            fail => result.IsBottom ? Ok() : BadRequest(fail.Message)
        );
    }
}
