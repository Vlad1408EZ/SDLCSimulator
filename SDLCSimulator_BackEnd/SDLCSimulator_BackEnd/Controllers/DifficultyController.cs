﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyService _difficultyService;

        public DifficultyController(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
        }

        /// <summary>
        /// Get task difficulties
        /// </summary>
        /// <returns>The list of task difficulties</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<DifficultyEnum>), StatusCodes.Status200OK)]
        public IActionResult GetTaskDifficulties()
        {
            try
            {
                var response = _difficultyService.GetAllDifficulties();

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
