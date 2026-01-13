using Api.Auth;
using Api.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<AppUser> users, SignInManager<AppUser> signIn, JwtTokenService jwt) : ControllerBase
{
    private readonly UserManager<AppUser> _users = users;
    private readonly SignInManager<AppUser> _signIn = signIn;
    private readonly JwtTokenService _jwt = jwt;

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest dto)
    {
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var res = await _users.CreateAsync(user, dto.Password);

        if (!res.Succeeded)
        {
            var errors = res.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());

            return BadRequest(new ValidationProblemDetails(errors));
        }

        return Ok(new RegisterResponse { Ok = true });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest dto)
    {
        var user = await _users.FindByEmailAsync(dto.Email);
        if (user is null) return Unauthorized();

        var ok = await _users.CheckPasswordAsync(user, dto.Password);
        if (!ok) return Unauthorized();

        var token = _jwt.CreateToken(user);
        return Ok(new LoginResponse { Token = token });
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(MeResponse), StatusCodes.Status200OK)]
    public ActionResult<MeResponse> Me()
    {
        var id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value;

        return Ok(new MeResponse { UserId = id, Email = email });
    }

    public record RegisterRequest(string Email, string Password);
    public record RegisterResponse { public bool Ok { get; init; } }

    public record LoginRequest(string Email, string Password);
    public record LoginResponse { public string Token { get; init; } = ""; }

    public record MeResponse { public string? UserId { get; init; } public string? Email { get; init; } }
}
