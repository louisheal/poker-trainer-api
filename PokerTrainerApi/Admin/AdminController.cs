using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using PokerTrainerApi.DrawRanges;
using PokerTrainerApi.DrawRanges.Repository;

namespace PokerTrainerApi.Admin;

[ApiController]
[Authorize(Roles = AdminAuthDefaults.Role)]
[Route("api/admin")]
public class AdminController(
    IConfiguration configuration,
    IRangeRepository repository) : ControllerBase
{
    [AllowAnonymous]
    [EnableRateLimiting(AdminAuthDefaults.LoginRateLimitPolicy)]
    [HttpPost("auth/login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Password))
        {
            return Unauthorized();
        }

        var expectedPassword = Encoding.UTF8.GetBytes(configuration["AdminAuth:Password"]!);
        var suppliedPassword = Encoding.UTF8.GetBytes(request.Password);
        if (!CryptographicOperations.FixedTimeEquals(expectedPassword, suppliedPassword))
        {
            return Unauthorized();
        }

        var expiresAt = DateTime.UtcNow.AddMinutes(30);
        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["AdminAuth:SigningKey"]!));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: AdminAuthDefaults.Issuer,
            audience: AdminAuthDefaults.Audience,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, "admin"),
                new Claim(ClaimTypes.Role, AdminAuthDefaults.Role)
            ],
            expires: expiresAt,
            signingCredentials: credentials);

        Response.Cookies.Append(
            AdminAuthDefaults.CookieName,
            new JwtSecurityTokenHandler().WriteToken(token),
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/api/admin",
                Expires = expiresAt,
                IsEssential = true
            });

        return NoContent();
    }

    [HttpGet("auth/session")]
    public IActionResult GetSession() => NoContent();

    [AllowAnonymous]
    [HttpPost("auth/logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(
            AdminAuthDefaults.CookieName,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/api/admin"
            });

        return NoContent();
    }

    [HttpGet("ranges/range")]
    public async Task<IActionResult> GetRange(string spotKey)
    {
        var range = await repository.GetRange(spotKey);
        return range == null ? NotFound() : Ok(range);
    }

    [HttpPost("ranges/range")]
    public async Task<IActionResult> UpdateRange(string spotKey, [FromBody] PokerRange range) =>
        Ok(await repository.UpdateRange(spotKey, range));
}

public record LoginRequest(string Password);