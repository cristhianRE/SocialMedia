using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin user)
        {
            // if is a valid user 
            if (IsValidUser(user))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }

            return NotFound();
        }

        private bool IsValidUser(UserLogin user)
        {
            // Logic to query DB and check if the user exists

            return true;
        }

        private string GenerateToken()
        {
            //Header
            var symetricSecurtyKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symetricSecurtyKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims - caracteristicas do usuario gerado
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Name"),
                new Claim(ClaimTypes.Email, "email@email.com"),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            // Payload

            var iss = _configuration["Authentication:Issuer"];
            var aud = _configuration["Authentication:Audience"];

            var payload = new JwtPayload(iss, aud, claims, DateTime.Now, DateTime.UtcNow.AddMinutes(2));

            // Generate token
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
