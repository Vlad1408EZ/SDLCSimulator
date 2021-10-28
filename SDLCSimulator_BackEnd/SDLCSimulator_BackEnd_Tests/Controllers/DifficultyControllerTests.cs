using Moq;
using SDLCSimulator_BackEnd.Controllers;
using SDLCSimulator_BusinessLogic.Interfaces;
using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SDLCSimulator_Data.Enums;
using Xunit;

namespace SDLCSimulator_BackEnd_Tests.Controllers
{
    public class DifficultyControllerTests
    {
        private readonly Mock<IDifficultyService> _difficultyService;
        private readonly DifficultyController _difficultyController;

        public DifficultyControllerTests()
        {
            _difficultyService = new Mock<IDifficultyService>();
            _difficultyController = new DifficultyController(_difficultyService.Object);
        }

        [Fact]
        public void GetTaskDifficulties_ValidResponse_ReturnOKResponse()
        {
            //arrange
            _difficultyService.Setup(d => d.GetAllDifficulties()).Returns(Enum.GetValues<DifficultyEnum>().ToList());

            //act
            var response = _difficultyController.GetTaskDifficulties();

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetTaskDifficulties_ThrowsException_ReturnBadRequestResponse()
        {
            //arrange
            _difficultyService.Setup(d => d.GetAllDifficulties()).Throws<InvalidOperationException>();

            //act
            var response = _difficultyController.GetTaskDifficulties();

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
