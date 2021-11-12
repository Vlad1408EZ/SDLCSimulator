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
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _taskService;
        private readonly TaskController _taskController;

        public TaskControllerTests()
        {
            _taskService = new Mock<ITaskService>();
            _taskController = new TaskController(_taskService.Object);
        }

        [Fact]
        public async Task GetFilteredTasksWithTaskResultsForStudentAsync_ClaimsContainUserIdAndGroupId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var studentId = 1;
            var groupId = 1;
            var input = TaskForStudentFilterInputFixture.CreateValidInput();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>(){new ("UserId", studentId.ToString()),
                        new("GroupId", groupId.ToString()) }))
            };
            _taskService.Setup(t => t.GetFilteredTasksWithTaskResultsForStudentAsync(input,groupId,studentId))
                .ReturnsAsync(new List<StudentTasksOutputModel>());

            //act
            var response = await _taskController.GetFilteredTasksWithTaskResultsForStudentAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetFilteredTasksWithTaskResultsForStudentAsync_ClaimsDoNotContainGroupId_ReturnBadRequestResponse()
        {
            //arrange

            var input = TaskForStudentFilterInputFixture.CreateValidInput();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _taskController.GetFilteredTasksWithTaskResultsForStudentAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді групи не валідне");
        }

        [Fact]
        public async Task GetFilteredTasksWithTaskResultsForStudentAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var groupId = 1;
            var input = TaskForStudentFilterInputFixture.CreateValidInput();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>(){new("GroupId", groupId.ToString())}))
            };

            //act
            var response = await _taskController.GetFilteredTasksWithTaskResultsForStudentAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді студента не валідне");
        }

        [Fact]
        public async Task GetFilteredTasksWithTaskResultsForStudentAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var studentId = 1;
            var groupId = 1;
            var input = TaskForStudentFilterInputFixture.CreateValidInput();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>(){new ("UserId", studentId.ToString()),
                        new("GroupId", groupId.ToString()) }))
            };
            _taskService.Setup(t => t.GetFilteredTasksWithTaskResultsForStudentAsync(input,groupId,studentId))
                .ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _taskController.GetFilteredTasksWithTaskResultsForStudentAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetFilteredTasksWithTaskResultsForTeacherAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var teacherId = 1;
            var input = TaskForTeacherFilterInputFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>(){new ("UserId", teacherId.ToString())}))
            };
            _taskService.Setup(t => t.GetFilteredTasksWithTaskResultsForTeacherAsync(input, teacherId))
                .ReturnsAsync(new List<TeacherTasksOutputModel>());

            //act
            var response = await _taskController.GetFilteredTasksWithTaskResultsForTeacherAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetFilteredTasksWithTaskResultsForTeacherAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var input = TaskForTeacherFilterInputFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _taskController.GetFilteredTasksWithTaskResultsForTeacherAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді вчителя не валідне");
        }

        [Fact]
        public async Task GetFilteredTasksWithTaskResultsForTeacherAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var teacherId = 1;
            var input = TaskForTeacherFilterInputFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>() { new("UserId", teacherId.ToString()) }))
            };
            _taskService.Setup(t => t.GetFilteredTasksWithTaskResultsForTeacherAsync(input,teacherId))
                .ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _taskController.GetFilteredTasksWithTaskResultsForTeacherAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateTaskAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var teacherId = 1;
            var input = CreateTaskInputModelFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _taskService.Setup(t => t.CreateTaskAsync(input, teacherId)).ReturnsAsync(new TeacherTasksOutputModel());

            //act
            var response = await _taskController.CreateTaskAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateTaskAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var input = CreateTaskInputModelFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _taskController.CreateTaskAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді вчителя не валідне");
        }

        [Fact]
        public async Task CreateTaskAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var teacherId = 1;
            var input = CreateTaskInputModelFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _taskService.Setup(t => t.CreateTaskAsync(input,teacherId)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _taskController.CreateTaskAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateTaskAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var teacherId = 1;
            var input = UpdateTaskInputModelFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _taskService.Setup(t => t.UpdateTaskAsync(input, teacherId)).ReturnsAsync(new TeacherTasksOutputModel());

            //act
            var response = await _taskController.UpdateTaskAsync(input);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateTaskAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var input = UpdateTaskInputModelFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _taskController.UpdateTaskAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді вчителя не валідне");
        }

        [Fact]
        public async Task UpdateTaskAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var teacherId = 1;
            var input = UpdateTaskInputModelFixture.CreateValidEntity();
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _taskService.Setup(t => t.UpdateTaskAsync(input, teacherId)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _taskController.UpdateTaskAsync(input);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task RemoveTaskAsync_ClaimsContainUserId_ValidResponse_ReturnOkResponse()
        {
            //arrange
            var teacherId = 1;
            var taskId = 1;
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _taskService.Setup(t => t.RemoveTaskAsync(teacherId, taskId)).ReturnsAsync(true);

            //act
            var response = await _taskController.RemoveTaskAsync(taskId);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RemoveTaskAsync_ClaimsDoNotContainUserId_ReturnBadRequestResponse()
        {
            //arrange
            var taskId = 1;
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>()))
            };

            //act
            var response = await _taskController.RemoveTaskAsync(taskId);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResponse = response as BadRequestObjectResult;
            badRequestResponse?.Value.ToString().Should().BeEquivalentTo("Айді вчителя не валідне");
        }

        [Fact]
        public async Task RemoveTaskAsync_InvalidResponse_ReturnBadRequestResponse()
        {
            //arrange
            var teacherId = 1;
            var taskId = 1;
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim> { new("UserId", teacherId.ToString()) }))
            };
            _taskService.Setup(t => t.RemoveTaskAsync(teacherId, taskId)).ThrowsAsync(new InvalidOperationException());

            //act
            var response = await _taskController.RemoveTaskAsync(taskId);

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
