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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// Get all groups.
        /// </summary>
        /// <returns>The list of groups</returns>
        [HttpGet("AllGroups")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGroupsAsync()
        {
            try
            {
                var response = await _groupService.GetAllGroupsAsync();

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get groups for teacher.
        /// </summary>
        /// <returns>The list of groups</returns>
        [HttpGet("TeacherGroups")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupsForTeacherAsync()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int teacherId);
                if (!success)
                    return BadRequest("Айді вчителя не валідне");
                var response = await _groupService.GetTeacherGroupsAsync(teacherId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Create a group.
        /// </summary>
        /// <param name="model">Group input model</param>
        /// <returns>Created group</returns>
        [HttpPost("CreateGroup")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateGroupAsync(CreateGroupInputModel model)
        {
            try
            {
                var response = await _groupService.CreateGroupAsync(model);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
