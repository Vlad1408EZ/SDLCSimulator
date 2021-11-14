using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace SDLCSimulator_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        /// <summary>
        /// Get task types.
        /// </summary>
        /// <returns>The list of task types</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<TaskTypeEnum>), StatusCodes.Status200OK)]
        public IActionResult GetTaskTypes()
        {
            try
            {
                var response = _typeService.GetAllTaskTypes();

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
