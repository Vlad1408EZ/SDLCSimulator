using FluentAssertions;
using FluentValidation;
using SDLCSimulator_BusinessLogic.Validators;
using SDLCSimulator_Common.Fixtures;
using System;
using Xunit;

namespace SDLCSimulator_BusinessLogic_Tests.Validators
{
    public class ChangePasswordRequestModelValidatorTests
    {
        //Тестує валідатор для моделі ChangePasswordRequestModel.
        //Мета тесту - переконатись в валідності об'єкту ChangePasswordRequestModel
        [Fact]
        public void Validate_ValidChangePasswordRequestModel_NotThrow()
        {
            //arrange
            var input = ChangePasswordRequestModelFixture.CreateValidEntity();
            var validator = new ChangePasswordRequestModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().NotThrow();
        }

        //Тестує валідатор для моделі ChangePasswordRequestModel.
        //Мета тесту - переконатись в невалідності паролю
        [Theory]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("passpasspasspasspasspasspasspasspasspasspasspasspass")]
        public void Validate_NotValidPassword_ThrowValidationException(string password)
        {
            //arrange
            var input = ChangePasswordRequestModelFixture.CreateValidEntity();
            input.NewPassword = password;
            var validator = new ChangePasswordRequestModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі ChangePasswordRequestModel.
        //Мета тесту - переконатись в тому, що новий пароль не має дорівнювати старому паролю
        [Fact]
        public void Validate_OldPasswordEqualsToNewPassword_ThrowValidationException()
        {
            //arrange
            var input = ChangePasswordRequestModelFixture.CreateValidEntity();
            input.OldPassword = "student12345";
            input.NewPassword = "student12345";
            var validator = new ChangePasswordRequestModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі ChangePasswordRequestModel.
        //Мета тесту - переконатись в тому, що підтвердження паролю має дорівнювати новому паролю
        [Fact]
        public void Validate_NewPasswordNotEqualsToConfirmPassword_ThrowValidationException()
        {
            //arrange
            var input = ChangePasswordRequestModelFixture.CreateValidEntity();
            input.NewPassword = "student12345";
            input.ConfirmPassword = "student1234";
            var validator = new ChangePasswordRequestModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }
    }
}
