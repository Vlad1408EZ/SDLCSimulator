using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SDLCSimulator_BackEnd.Controllers;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_Data.Enums;
using Xunit;

namespace SDLCSimulator_BackEnd_Tests.Controllers
{
    public class TypeControllerTests
    {
        private readonly Mock<ITypeService> _typeService;
        private readonly TypeController _typeController;

        public TypeControllerTests()
        {
            _typeService = new Mock<ITypeService>();
            _typeController = new TypeController(_typeService.Object);
        }

        [Fact]
        public void GetTaskTypes_ValidResponse_ReturnOKResponse()
        {
            //arrange
            _typeService.Setup(d => d.GetAllTaskTypes()).Returns(Enum.GetValues<TaskTypeEnum>().ToList());

            //act
            var response = _typeController.GetTaskTypes();

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetTaskDifficulties_ThrowsException_ReturnBadRequestResponse()
        {
            //arrange
            _typeService.Setup(d => d.GetAllTaskTypes()).Throws<InvalidOperationException>();

            //act
            var response = _typeController.GetTaskTypes();

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
