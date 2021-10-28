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
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;
using Xunit;

namespace SDLCSimulator_BackEnd_Tests.Controllers
{
    public class TaskResultControllerTests
    {
        private readonly Mock<ITaskResultService> _taskResultService;
        private readonly TaskResultController _taskResultController;

        public TaskResultControllerTests()
        {
            _taskResultService = new Mock<ITaskResultService>();
            _taskResultController = new TaskResultController(_taskResultService.Object);
        }

        [Fact]
        public async Task SetTaskResultAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var studentId = 1;
            var input = CreateTaskResultInputFixture.CreateValidEntity();
            _taskResultController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", studentId.ToString()) }))
            };
            _taskResultService.Setup(t => t.SetTaskResultAsync(input,studentId))
                .ReturnsAsync(new StudentTaskResultOutputModel());

            //act
            var response = await _taskResultController.SetTaskResultAsync(input);

            //arrange
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task SetTaskResultAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var input = CreateTaskResultInputFixture.CreateValidEntity();
            _taskResultController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _taskResultController.SetTaskResultAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді студента не валідне");
        }

        [Fact]
        public async Task SetTaskResultAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var studentId = 1;
            var input = CreateTaskResultInputFixture.CreateValidEntity();
            _taskResultController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", studentId.ToString()) }))
            };
            _taskResultService.Setup(t => t.SetTaskResultAsync(input,studentId))
                .ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _taskResultController.SetTaskResultAsync(input);

            //arrange
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
