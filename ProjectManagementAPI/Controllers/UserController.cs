﻿using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Interfaces;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/v1/user/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        // GET: api/v1/user/1
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            return await _userService.GetUser(id);
        }

        // POST: api/v1/user
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/v1/user/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(long id, User user)
        {
            await _userService.UpdateUser(id, user);
            return NoContent();
        }

        // DELETE: api/v1/user/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(long id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
