using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="model">Input authentication model</param>
        /// <returns>AuthenticateResponseModel</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(AuthenticateResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync(AuthenticateRequestModel model)
        {
            try
            {
                var response = await _userService.LoginAsync(model);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update current user info.
        /// </summary>
        /// <param name="model">Current user update model</param>
        /// <returns>Updated info</returns>
        [HttpPut("UpdateUserInfo")]
        //[Authorize]
        [ProducesResponseType(typeof(UpdateUserInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserInfoAsync(UpdateUserInfoModel model)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("The user id is not valid");

                var response = await _userService.UpdateUserInfoAsync(model,userId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="model">New password with confirmation</param>
        /// <returns>true or false</returns>
        [HttpPost("ChangePassword")]
        //[Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool success = int.TryParse(identity?.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out int userId);
                if (!success)
                    return BadRequest("The user id is not valid");

                var response = await _userService.ChangePasswordAsync(model,userId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
