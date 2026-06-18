using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevFreela.API.Authentication;
using DevFreela.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtOptions _jwtOptions;

        public AuthController(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> Login(LoginInputModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.Email) || string.IsNullOrWhiteSpace(inputModel.Password))
            {
                return BadRequest("Email e senha sao obrigatorios.");
            }

            if (!string.IsNullOrWhiteSpace(_jwtOptions.DemoPassword) && inputModel.Password != _jwtOptions.DemoPassword)
            {
                return Unauthorized();
            }

            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, inputModel.Email),
                new Claim(JwtRegisteredClaimNames.Email, inputModel.Email),
                new Claim(ClaimTypes.Role, inputModel.Role)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResponse(accessToken, expiresAt));
        }
    }
}
