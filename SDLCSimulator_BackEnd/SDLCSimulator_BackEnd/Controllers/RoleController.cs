using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Get roles.
        /// </summary>
        /// <returns>The list of roles</returns>
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<RoleEnum>), StatusCodes.Status200OK)]
        public IActionResult GetRoles()
        {
            try
            {
                var response = _roleService.GetAllRoles();

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
