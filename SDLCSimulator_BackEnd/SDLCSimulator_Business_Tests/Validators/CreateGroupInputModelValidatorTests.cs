using FluentAssertions;
using FluentValidation;
using SDLCSimulator_BusinessLogic.Validators;
using SDLCSimulator_Common.Fixtures;
using System;
using Xunit;

namespace SDLCSimulator_BusinessLogic_Tests.Validators
{
    public class CreateGroupInputModelValidatorTests
    {
        //Тестує валідатор для моделі CreateGroupInputModel.
        //Мета тесту - переконатись в валідності об'єкту CreateGroupInputModel
        [Fact]
        public void Validate_CreateGroupInputModel_NotThrow()
        {
            //arrange
            var input = CreateGroupInputModelFixture.CreateValidEntity();
            var validator = new CreateGroupInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().NotThrow();
        }

        //Тестує валідатор для моделі CreateGroupInputModel.
        //Мета тесту - переконатись в невалідності назви групи
        [Fact]
        public void Validate_InvalidGroupName_ThrowValidationException()
        {
            //arrange
            var input = CreateGroupInputModelFixture.CreateValidEntity();
            input.GroupName = "Invalid group";
            var validator = new CreateGroupInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }
    }
}
