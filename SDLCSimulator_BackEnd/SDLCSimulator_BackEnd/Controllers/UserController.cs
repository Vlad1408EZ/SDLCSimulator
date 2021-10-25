using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                if (response == null)
                    return BadRequest("Login or password is incorrect");

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
