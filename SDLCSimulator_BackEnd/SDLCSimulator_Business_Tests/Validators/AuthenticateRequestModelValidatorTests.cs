using FluentAssertions;
using FluentValidation;
using SDLCSimulator_BusinessLogic.Validators;
using SDLCSimulator_Common.Fixtures;
using System;
using Xunit;

namespace SDLCSimulator_BusinessLogic_Tests.Validators
{
    public class AuthenticateRequestModelValidatorTests
    {
        //Тестує валідатор для моделі AuthenticateRequestModel.
        //Мета тесту - переконатись в валідності об'єкту AuthenticateRequestModel
        [Fact]
        public void Validate_ValidAuthenticateRequestModel_NotThrow()
        {
            //arrange
            var input = AuthenticateRequestModelFixture.CreateValidEntity();
            var validator = new AuthenticateRequestModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().NotThrow();
        }

        //Тестує валідатор для моделі AuthenticateRequestModel.
        //Мета тесту - переконатись в невалідності імейлу
        [Theory]
        [InlineData(null)]
        [InlineData("test")]
        public void Validate_NotValidEmail_ThrowValidationException(string emailAddress)
        {
            //arrange
            var input = AuthenticateRequestModelFixture.CreateValidEntity();
            input.Email = emailAddress;
            var validator = new AuthenticateRequestModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі AuthenticateRequestModel.
        //Мета тесту - переконатись в невалідності паролю
        [Theory]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("passpasspasspasspasspasspasspasspasspasspasspasspass")]
        public void Validate_NotValidPassword_ThrowValidationException(string password)
        {
            //arrange
            var input = AuthenticateRequestModelFixture.CreateValidEntity();
            input.Password = password;
            var validator = new AuthenticateRequestModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }
    }
}
