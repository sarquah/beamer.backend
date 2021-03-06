﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Beamer.Domain.Models;
using Beamer.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Beamer.API.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/v1/user/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(Guid tenantId)
        {
            var users = await _userService.GetUsers(tenantId);
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(usersDTO);
        }

        // GET: api/v1/user/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> GetUser(long id, Guid tenantId)
        {
            var user = await _userService.GetUser(id, tenantId);
            if (user == null)
            {
                return NotFound();
            }
            var userDetailsDTO = _mapper.Map<UserDetailsDTO>(user);
            return userDetailsDTO;
        }

        // POST: api/v1/user
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            bool success = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // POST: api/v1/user/users
        [HttpPost("users")]
        public async Task<ActionResult> CreateUsers(IEnumerable<User> users)
        {
            bool success = await _userService.CreateUsers(users);
            return CreatedAtAction(nameof(GetUsers), users);
        }

        // PUT: api/v1/user/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(long id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            bool success = await _userService.UpdateUser(id, user);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        // DELETE: api/v1/user/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(long id)
        {
            bool success = await _userService.DeleteUser(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
