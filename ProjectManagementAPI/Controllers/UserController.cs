using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.API.Controllers
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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        // GET: api/v1/user/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> GetUser(long id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST: api/v1/user
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            bool success = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
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
            return NoContent();
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
            return NoContent();
        }
    }
}
