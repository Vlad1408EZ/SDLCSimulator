using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SDLCSimulator_BackEnd.Controllers;
using SDLCSimulator_BackEnd_Tests.Fixtures;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_BusinessLogic.Models.Output;
using Xunit;

namespace SDLCSimulator_BackEnd_Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userService = new Mock<IUserService>();
            _userController = new UserController(_userService.Object);
        }

        [Fact]
        public async Task LoginAsync_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var input = AuthenticateRequestModelFixture.CreateValidEntity();
            _userService.Setup(u => u.LoginAsync(input)).ReturnsAsync(new AuthenticateResponseModel());

            //act
            var response = await _userController.LoginAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task LoginAsync_InvalidResponse_ReturnOkResponse()
        {
            //arrange
            var input = AuthenticateRequestModelFixture.CreateValidEntity();
            _userService.Setup(u => u.LoginAsync(input)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _userController.LoginAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetAllUsersAsync_ValidResponse_ReturnOkResponse()
        {
            //arrange
            _userService.Setup(u => u.GetAllUsersAsync()).ReturnsAsync(new List<UserOutputModel>());

            //act
            var response = await _userController.GetAllUsersAsync();

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllUsersAsync_InvalidResponse_ReturnOkResponse()
        {
            //arrange
            _userService.Setup(u => u.GetAllUsersAsync()).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _userController.GetAllUsersAsync();

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateUserAsync_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            _userService.Setup(u => u.CreateUserAsync(input)).ReturnsAsync(new UserOutputModel());

            //act
            var response = await _userController.CreateUserAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateUserAsync_InvalidResponse_ReturnOkResponse()
        {
            //arrange
            var input = CreateUserInputModelFixture.CreateValidStudent();
            _userService.Setup(u => u.CreateUserAsync(input)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _userController.CreateUserAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateUserInfoAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var userId = 1;
            var input = UpdateUserInfoModelFixture.CreateValidEntity();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", userId.ToString()) }))
            };
            _userService.Setup(u => u.UpdateUserInfoAsync(input, userId)).ReturnsAsync(new UpdateUserInfoModel());

            //act
            var response = await _userController.UpdateUserInfoAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateUserInfoAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var input = UpdateUserInfoModelFixture.CreateValidEntity();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _userController.UpdateUserInfoAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді користувача не валідне");
        }

        [Fact]
        public async Task UpdateUserAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var userId = 1;
            var input = UpdateUserInfoModelFixture.CreateValidEntity();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", userId.ToString()) }))
            };
            _userService.Setup(u => u.UpdateUserInfoAsync(input,userId)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _userController.UpdateUserInfoAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ChangePasswordAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var userId = 1;
            var input = ChangePasswordRequestModelFixture.CreateValidEntity();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", userId.ToString()) }))
            };
            _userService.Setup(u => u.ChangePasswordAsync(input, userId)).ReturnsAsync(true);

            //act
            var response = await _userController.ChangePasswordAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ChangePasswordAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var input = ChangePasswordRequestModelFixture.CreateValidEntity();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _userController.ChangePasswordAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді користувача не валідне");
        }

        [Fact]
        public async Task ChangePasswordAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var userId = 1;
            var input = ChangePasswordRequestModelFixture.CreateValidEntity();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", userId.ToString()) }))
            };
            _userService.Setup(u => u.ChangePasswordAsync(input, userId)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _userController.ChangePasswordAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
