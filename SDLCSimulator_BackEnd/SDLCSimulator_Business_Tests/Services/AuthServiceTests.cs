using Microsoft.Extensions.Options;
using SDLCSimulator_BusinessLogic.Models.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using FluentAssertions;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Services;
using Xunit;
using SDLCSimulator_Common.Fixtures;

namespace SDLCSimulator_BusinessLogic_Tests
{
    public class AuthServiceTests
    {
        private readonly IOptions<JwtConfig> _jwtConfig;
        private readonly IAuthService _authService;
        public AuthServiceTests()
        {
            _jwtConfig = Options.Create(new JwtConfig()
            {
                Issuer = "User",
                ExpirationDate = 365,
                Key = "bRhYJRlZvBj2vW4MrV5HVdPgIE6VMtCFB0kTtJ1m"
            });
            _authService = new AuthService(_jwtConfig);
        }

        [Fact]
        public void GenerateWebTokenForUser_GeneratedSuccessfully_ReturnTokenWith7Claims()
        {
            //arrange
            var user = UserFixture.CreateValidEntity();

            //act
            var token = _authService.GenerateWebTokenForUser(user);

            //assert
            token.Should().NotBeNullOrEmpty();
            var extractedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            extractedToken.Claims.Should().HaveCount(7);
        }
    }
}
