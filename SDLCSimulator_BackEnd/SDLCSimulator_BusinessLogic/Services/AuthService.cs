using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Configuration;
using SDLCSimulator_Data;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<JwtConfig> _jwtConfig;

        public AuthService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string GenerateWebTokenForUser(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Key));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim("UserId",user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                _jwtConfig.Value.Issuer,
                _jwtConfig.Value.Issuer,
                claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(_jwtConfig.Value.ExpirationDate));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }
    }
}
