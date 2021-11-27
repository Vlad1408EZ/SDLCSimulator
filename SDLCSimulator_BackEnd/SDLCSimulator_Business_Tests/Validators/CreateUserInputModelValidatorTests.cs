using FluentAssertions;
using FluentValidation;
using SDLCSimulator_BusinessLogic.Validators;
using SDLCSimulator_Common.Fixtures;
using SDLCSimulator_Data.Enums;
using System;
using System.Collections.Generic;
using Xunit;


namespace SDLCSimulator_BusinessLogic_Tests.Validators
{
    public class CreateUserInputModelValidatorTests
    {
        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в валідності об'єкту CreateUserInputModel для студента
        [Fact]
        public void Validate_ValidCreateUserInputModel_Student_NotThrow()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().NotThrow();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в валідності об'єкту CreateUserInputModel для студента
        [Fact]
        public void Validate_ValidCreateUserInputModel_Teacher_NotThrow()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidTeacher();
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().NotThrow();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в невалідності імені
        [Theory]
        [InlineData("іван")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("іваніваніваніваніваніваніваніваніваніваніваніваніван")]
        public void Validate_InvalidFirstName_ThrowValidationException(string firstName)
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidTeacher();
            input.FirstName = firstName;
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в невалідності прізвища
        [Theory]
        [InlineData("іванов")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("івановівановівановівановівановівановівановівановіванов")]
        public void Validate_InvalidLastName_ThrowValidationException(string lastName)
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidTeacher();
            input.LastName = lastName;
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в невалідності паролю
        [Theory]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("passpasspasspasspasspasspasspasspasspasspasspasspass")]
        public void Validate_InvalidPassword_ThrowValidationException(string password)
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidTeacher();
            input.Password = password;
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в невалідності імейлу для студента
        [Fact]
        public void Validate_InvalidEmailForStudent_ThrowValidationException()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            input.Email = "іван.іванов.викладач@lpnu.ua";
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в невалідності імейлу для викладача
        [Fact]
        public void Validate_InvalidEmailForTeacher_ThrowValidationException()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidTeacher();
            input.Email = "андрій.фоменко.пз@lpnu.ua";
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в невалідності груп для студента
        [Fact]
        public void Validate_InvalidGroupsForStudent_ThrowValidationException()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            input.Groups.Add("ПЗ-42");
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в тому, що кількість груп для викладача має бути більше 0
        [Fact]
        public void Validate_EmptyGroupsForTeacher_ThrowValidationException()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            input.Groups = new List<string>();
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в тому, що групи для викладача не мають повторюватись
        [Fact]
        public void Validate_GroupsAreNotUniqueForTeacher_ThrowValidationException()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            input.Groups.Add("ПЗ-41");
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }

        //Тестує валідатор для моделі CreateUserInputModel.
        //Мета тесту - переконатись в тому, що не можна створити адміна
        [Fact]
        public void Validate_InvalidRole_ThrowValidationException()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            input.Role = RoleEnum.Admin;
            var validator = new CreateUserInputModelValidator();

            //act
            Action act = () => validator.ValidateAndThrow(input);

            //assert
            act.Should().Throw<ValidationException>();
        }
    }
}
