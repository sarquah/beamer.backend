using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Interfaces;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/v1/user/users
        [HttpGet("users")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        // GET: api/v1/user/1
        [HttpGet("{id}")]
        public async Task<User> GetUser(long id)
        {
            return await _userService.GetUser(id);
        }

        // POST: api/v1/user
        [HttpPost]
        public void CreateUser(User user)
        {
            _userService.CreateUser(user);
        }

        // PUT: api/v1/user/1
        [HttpPut("{id}")]
        public void UpdateUser(long id, User user)
        {
            _userService.UpdateUser(id, user);
        }

        // DELETE: api/v1/user/1
        [HttpDelete("{id}")]
        public void DeleteUser(long id)
        {
            _userService.DeleteUser(id);
        }
    }
}
