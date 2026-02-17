using Microsoft.AspNetCore.Mvc;
using RuangApp.Api.Dtos;
using RuangApp.Api.Services;

namespace RuangApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IJwtService jwtService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IJwtService _jwtService = jwtService;

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            return BadRequest(new { message = "Username dan password harus diisi" });

        var user = await _authService.AuthenticateAsync(request.Username, request.Password);
        if (user == null)
            return Unauthorized(new { message = "Username atau password salah" });

        var token = _jwtService.GenerateToken(user);
        var response = new LoginResponse
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            }
        };

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest(new { message = "Username, email, dan password harus diisi" });

        var user = await _authService.RegisterAsync(request.Username, request.Email, request.Password);
        if (user == null)
            return BadRequest(new { message = "Username atau email sudah terdaftar" });

        var token = _jwtService.GenerateToken(user);
        var response = new LoginResponse
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            }
        };

        return Ok(response);
    }
}
