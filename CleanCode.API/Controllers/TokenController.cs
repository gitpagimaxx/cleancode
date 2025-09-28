using CleanCode.API.Models;
using CleanCode.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanCode.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController(
    ILogger<TokenController> logger, 
    IAuthenticate authenticate,
    IConfiguration configuration) : ControllerBase
{
    private readonly ILogger<TokenController> _logger = logger;
    private readonly IAuthenticate _authenticate = authenticate;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("LoginUser")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser([FromBody] LoginModel userInfo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var isAuthenticated = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

        if (isAuthenticated)
        {
            var token = GerenerateToken(userInfo);
            return Ok(token);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Unauthorized(ModelState);
        }
    }

    [HttpPost("RegisterUser")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel userInfo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var isRegistered = await _authenticate.RegisterUser(userInfo.Email!, userInfo.Password!);
        if (isRegistered)
        {
            return Ok("User registered successfully.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Registration failed. User may already exist.");
            return BadRequest(ModelState);
        }
    }

    private UserToken GerenerateToken(LoginModel userInfo)
    {
        var claims = new[]
        {
            new Claim("email", userInfo.Email),
            new Claim("MeuValor", "UmSegredoSuperForteCom32CaracteresOuMais"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(10);

        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new UserToken() 
        { 
            Token = new JwtSecurityTokenHandler().WriteToken(token), 
            Expiration = expiration 
        };
    }
}
