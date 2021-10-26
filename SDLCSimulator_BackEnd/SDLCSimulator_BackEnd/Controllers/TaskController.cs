using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;

namespace SDLCSimulator_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get tasks with task results for student.
        /// </summary>
        /// /// <param name="filterInput">Task filtering</param>
        /// <returns>Tasks with task results for student.</returns>             
        [HttpGet("StudentTasks")]
        //[Authorize(Roles = "Student")]
        [ProducesResponseType(typeof(List<StudentTasksOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilteredTasksWithTaskResultsForStudentAsync([FromQuery] TaskForStudentFilterInput filterInput)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "GroupId")?.Value, out int groupId);
                if (!success)
                    return BadRequest("The group id is not valid");

                success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("The user id is not valid");

                var result =
                    await _taskService.GetFilteredTasksWithTaskResultsForStudentAsync(filterInput, groupId, userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
