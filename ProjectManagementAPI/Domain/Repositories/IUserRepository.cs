using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementAPI.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(long id);
        Task CreateUser(User user);
        Task UpdateUser(long id, User user);
        Task DeleteUser(long id);
    }
}
