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
        /// <param name="filterInput">Task filtering</param>
        /// <returns>Tasks with task results for student.</returns>             
        [HttpGet("StudentTasks")]
        [Authorize(Roles = "Student")]
        [ProducesResponseType(typeof(List<StudentTasksOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilteredTasksWithTaskResultsForStudentAsync([FromQuery] TaskForStudentFilterInput filterInput)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "GroupId")?.Value, out int groupId);
                if (!success)
                    return BadRequest("Айді групи не валідне");

                success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("Айді студента не валідне");

                var result =
                    await _taskService.GetFilteredTasksWithTaskResultsForStudentAsync(filterInput, groupId, userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get tasks with task results for teacher.
        /// </summary>
        /// <param name="filterInput">Task filtering</param>
        /// <returns>Tasks with task results for teacher.</returns>             
        [HttpGet("TeacherTasks")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(List<StudentTasksOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilteredTasksWithTaskResultsForTeacherAsync([FromQuery] TaskForTeacherFilterInput filterInput)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("Айді вчителя не валідне");

                var result =
                    await _taskService.GetFilteredTasksWithTaskResultsForTeacherAsync(filterInput,userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Create task.
        /// </summary>
        /// <param name="model">Task input model</param>
        /// <returns>Created task.</returns>             
        [HttpPost("CreateTask")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(TeacherTasksOutputModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTaskAsync(CreateTaskInputModel model)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("Айді вчителя не валідне");

                var result = await _taskService.CreateTaskAsync(model,userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update task.
        /// </summary>
        /// <param name="model">Task input model</param>
        /// <returns>Updated task.</returns>             
        [HttpPut("UpdateTask")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(TeacherTasksOutputModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTaskAsync(UpdateTaskInputModel model)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("Айді вчителя не валідне");

                var result = await _taskService.UpdateTaskAsync(model,userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete task.
        /// </summary>
        /// <param name="taskId">Id of task to delete</param>
        /// <returns>true or false.</returns>             
        [HttpDelete("RemoveTask")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveTaskAsync(int taskId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("Айді вчителя не валідне");

                var result = await _taskService.RemoveTaskAsync(userId,taskId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
