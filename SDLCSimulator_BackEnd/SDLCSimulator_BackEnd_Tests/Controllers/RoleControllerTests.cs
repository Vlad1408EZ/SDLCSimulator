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
    public class RoleControllerTests
    {
        private readonly Mock<IRoleService> _roleService;
        private readonly RoleController _roleController;

        public RoleControllerTests()
        {
            _roleService = new Mock<IRoleService>();
            _roleController = new RoleController(_roleService.Object);
        }

        [Fact]
        public void GetRoles_ValidResponse_ReturnOkResponse()
        {
            //arrange
            _roleService.Setup(d => d.GetAllRoles()).Returns(Enum.GetValues<RoleEnum>().ToList());

            //act
            var response = _roleController.GetRoles();

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetRoles_ThrowsException_ReturnBadRequestResponse()
        {
            //arrange
            _roleService.Setup(d => d.GetAllRoles()).Throws<InvalidOperationException>();

            //act
            var response = _roleController.GetRoles();

            //assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
