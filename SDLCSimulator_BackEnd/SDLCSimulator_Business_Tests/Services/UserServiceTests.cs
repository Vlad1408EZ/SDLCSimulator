using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Configuration;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_BusinessLogic.Services;
using SDLCSimulator_Common;
using SDLCSimulator_Common.Fixtures;
using SDLCSimulator_Data;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SDLCSimulator_BusinessLogic_Tests.Services
{
    public class UserServiceTests
    {
        private readonly SDLCSimulatorDbContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly GroupRepository _groupRepository;
        private readonly AuthService _authService;
        private readonly Mock<IEmailService> _emailService;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<SDLCSimulatorDbContext>()
               .UseInMemoryDatabase(databaseName: "UserTests").Options;
            _dbContext = new SDLCSimulatorDbContext(options);
            var _dbContextFactory = new Mock<IDbContextFactory<SDLCSimulatorDbContext>>();
            _dbContextFactory.Setup(d => d.CreateDbContext()).Returns(_dbContext);
            _userRepository = new UserRepository(_dbContextFactory.Object);
            _groupRepository = new GroupRepository(_dbContextFactory.Object);
            _authService = new AuthService(Options.Create(new JwtConfig
            {
                Issuer = "Test",
                Key = "bRhYJRlZvBj2vW4MrV5HVdPgIE6VMtCFB0kTtJ1m",
                ExpirationDate = 365
            }));
            _emailService = new Mock<IEmailService>();
            _userService = new UserService(_userRepository, _authService, _groupRepository, _emailService.Object);
        }

        //Тестує функцію LoginAsync класу UserService.
        //Мета тесту - переконатись в обробці ексепшну при неіснучому імейлу юзера
        [Fact]
        public async System.Threading.Tasks.Task LoginAsync_UserNotFound_ThrowsInvalidOperationException()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = AuthenticateRequestModelFixture.CreateValidEntity();
            input.Email = "Invalid email";

            //act
            Func<Task<AuthenticateResponseModel>> act = () =>  _userService.LoginAsync(input);

            //assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Користувач не знайдений або введено неправильний пароль");
        }

        //Тестує функцію LoginAsync класу UserService.
        //Мета тесту - переконатись в обробці ексепшну при невалідному паролю
        [Fact]
        public async System.Threading.Tasks.Task LoginAsync_InvalidPassword_ThrowsInvalidOperationException()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = AuthenticateRequestModelFixture.CreateValidEntity();
            input.Password = "student12345";

            //act
            Func<Task<AuthenticateResponseModel>> act = () => _userService.LoginAsync(input);

            //assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Користувач не знайдений або введено неправильний пароль");
        }

        //Тестує функцію LoginAsync класу UserService.
        //Мета тесту - переконатись в тому, що юзер успішно залогінився
        [Fact]
        public async System.Threading.Tasks.Task LoginAsync_ValidInput_NotThrow()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = AuthenticateRequestModelFixture.CreateValidEntity();

            //act
            Func<Task<AuthenticateResponseModel>> act = () => _userService.LoginAsync(input);

            //assert
            await act.Should().NotThrowAsync();
        }
    }
}
