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
using SDLCSimulator_BusinessLogic.Helpers;
using SDLCSimulator_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SDLCSimulator_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskService taskService, ITaskRepository taskRepository)
        {
            _taskService = taskService;
            _taskRepository = taskRepository;
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

                var tasks = await _taskRepository.GetTasksWithTaskResultsForStudent(groupId, userId).ToListAsync();
                if (filterInput != null)
                {
                    if (filterInput.Difficulties != null)
                    {
                        tasks = tasks.Where(t => filterInput.Difficulties.Any(d => d == t.Difficulty)).ToList();
                    }

                    if (filterInput.Types != null)
                    {
                        tasks = tasks.Where(t => filterInput.Types.Any(d => d == t.Type)).ToList();
                    }

                    if (!string.IsNullOrEmpty(filterInput.Topic))
                    {
                        tasks = tasks.Where(t => t.Topic.ToLower().StartsWith(filterInput.Topic.ToLower())).ToList();
                    }
                }

                var result = tasks.Select(t => new StudentTasksOutputModel()
                {
                    Id = t.Id,
                    Difficulty = t.Difficulty,
                    Type = t.Type,
                    Topic = t.Topic,
                    Description = t.Description,
                    Standard = t.Standard,
                    ErrorRate = ErrorRateGetter.GetErrorRate(t.ErrorRate),
                    MaxGrade = (int)t.MaxGrade,
                    TaskTime = (int)t.TaskTime,
                    TeacherFirstName = t.Teacher.FirstName,
                    TeacherLastName = t.Teacher.LastName,
                    StudentsTaskResults = t.TaskResults.Select(tr => new StudentTaskResultOutputModel()
                    {
                        Id = tr.Id,
                        ErrorCount = tr.ErrorCount,
                        Percentage = tr.Percentage,
                        FinalMark = tr.FinalMark,
                        Result = tr.Result
                    }).ToList()
                }).ToList();

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
        [ProducesResponseType(typeof(List<TeacherTasksOutputModel>), StatusCodes.Status200OK)]
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
