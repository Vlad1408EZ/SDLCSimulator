using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SDLCSimulator_BackEnd.Controllers;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_Common.Fixtures;
using Xunit;

namespace SDLCSimulator_BackEnd_Tests.Controllers
{
    public class GroupControllerTests
    {
        private readonly Mock<IGroupService> _groupService;
        private readonly GroupController _groupController;

        public GroupControllerTests()
        {
            _groupService = new Mock<IGroupService>();
            _groupController = new GroupController(_groupService.Object);
        }

        [Fact]
        public async Task GetAllGroupsAsync_ValidResponse_ReturnOkResponse()
        {
            //arrange
            _groupService.Setup(g => g.GetAllGroupsAsync()).ReturnsAsync(new List<GroupOutputModel>());

            //act
            var response = await _groupController.GetAllGroupsAsync();

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllGroupsAsync_ThrowsException_ReturnBadRequestResponse()
        {
            //arrange
            _groupService.Setup(g => g.GetAllGroupsAsync()).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _groupController.GetAllGroupsAsync();

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetGroupsForTeacherAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var teacherId = 1;
            _groupController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _groupService.Setup(g => g.GetTeacherGroupsAsync(teacherId)).ReturnsAsync(new List<GroupOutputModel>());

            //act
            var response = await _groupController.GetGroupsForTeacherAsync();

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetGroupsForTeacherAsync_ClaimsDoNotContainUserId_ValidResponse_ReturnBadRequestResponse()
        {
            //arrange
            _groupController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _groupController.GetGroupsForTeacherAsync();

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді вчителя не валідне");
        }

        [Fact]
        public async Task GetGroupsForTeacherAsync_ThrowsException_ReturnBadRequestResponse()
        {
            //arrange
            var teacherId = 1;
            _groupController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _groupService.Setup(g => g.GetTeacherGroupsAsync(teacherId)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _groupController.GetGroupsForTeacherAsync();

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateGroupAsync_ValidInputModelAndResponse_ReturnOkResponse()
        {
            //arrange
            var input = CreateGroupInputModelFixture.CreateValidEntity();
            _groupService.Setup(g => g.CreateGroupAsync(input))
                .ReturnsAsync(new GroupOutputModel());

            //act
            var response = await _groupController.CreateGroupAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateGroupAsync_ValidInputModelAndInvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var input = CreateGroupInputModelFixture.CreateValidEntity();
            _groupService.Setup(g => g.CreateGroupAsync(input))
                .ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _groupController.CreateGroupAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
