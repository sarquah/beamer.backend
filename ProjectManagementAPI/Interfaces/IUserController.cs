using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Interfaces
{
    interface IUserController
    {
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<ActionResult<User>> GetUser(long id);
        Task<ActionResult<User>> CreateUser(User user);
        Task<IActionResult> UpdateUser(long id, User user);
        Task<IActionResult> DeleteUser(long id);
    }
}
