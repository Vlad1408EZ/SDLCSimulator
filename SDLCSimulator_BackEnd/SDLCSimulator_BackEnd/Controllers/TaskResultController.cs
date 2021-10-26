using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;

namespace SDLCSimulator_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskResultController : ControllerBase
    {
        private readonly ITaskResultService _taskResultService;

        public TaskResultController(ITaskResultService taskResultService)
        {
            _taskResultService = taskResultService;
        }

        /// <summary>
        /// Set task result and get final mark.
        /// </summary>
        /// <param name="model">Input task result model</param>
        /// <returns>Task result information</returns>
        [HttpPost("SetTaskResult")]
        [ProducesResponseType(typeof(StudentTaskResultOutputModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetTaskResultAsync(TaskResultInput model)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("The user id is not valid");
                var response = await _taskResultService.SetTaskResultAsync(model,userId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
