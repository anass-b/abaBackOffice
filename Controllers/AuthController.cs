using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Concurrent;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private static readonly ConcurrentDictionary<int, string> _refreshTokens = new();
        private static readonly object _lock = new();

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _authService.AuthenticateAsync(model.Email, model.Password);
            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            var (token, expiresAt) = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            lock (_lock)
            {
                _refreshTokens[user.Id] = refreshToken;
            }

            return Ok(new { Token = token, RefreshToken = refreshToken, ExpiresAt = expiresAt });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenModel model)
        {
            var principal = GetPrincipalFromExpiredToken(model.Token);
            var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            lock (_lock)
            {
                if (!_refreshTokens.TryGetValue(userId, out var savedRefreshToken) || savedRefreshToken != model.RefreshToken)
                {
                    return Unauthorized(new { Message = "Invalid refresh token" });
                }

                var newToken = GenerateJwtToken(principal.Claims);
                var newRefreshToken = GenerateRefreshToken();
                _refreshTokens[userId] = newRefreshToken;

                return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
            }
        }

        [HttpPost("validate")]
        public IActionResult ValidateToken()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader)) return Unauthorized(new { IsAuthorized = false });

            var token = authHeader.Replace("Bearer ", "");
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(new { IsAuthorized = true, UserId = userId });
            }
            catch
            {
                return Unauthorized(new { IsAuthorized = false });
            }
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto model)
        {
            var user = await _authService.AuthenticateAsync(model.Email, model.CurrentPassword);
            if (user == null)
                return Unauthorized(new { Message = "Invalid current credentials" });

            var success = await _authService.UpdatePasswordAsync(user.Id, model.NewPassword);
            if (!success)
                return StatusCode(500, new { Message = "Password update failed" });

            return Ok(new { Message = "Password updated successfully" });
        }


        private (string Token, DateTime ExpiresAt) GenerateJwtToken(UserDto user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var expiresAt = DateTime.UtcNow.AddHours(2);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("isadmin", user.IsAdmin.ToString().ToLower())
                // Add more claims if needed
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), expiresAt);
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var validationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(token, validationParams, out _);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
                