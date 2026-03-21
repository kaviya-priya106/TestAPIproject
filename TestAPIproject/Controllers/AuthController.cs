using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestAPIproject.Models;
using TestAPIproject.Service;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthService _authService;

    public AuthController(IConfiguration config,IAuthService service)
    {
        _config = config;
        _authService = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {

        try
        {
            var token = await _authService.LoginAsync(dto.Username, dto.Password);

            return Ok(new
            {
                Token = token
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                Message = ex.Message
            });
        }
    }

 
}