using Api.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
[Authorize]
public class AuthController(SignInManager<AppUser> signInManager) : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }

}
