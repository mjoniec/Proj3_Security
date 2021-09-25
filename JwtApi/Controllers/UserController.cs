﻿using JwtApi.Model;
using JwtApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            var result = await _userService.CreateUserAsync(user);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}