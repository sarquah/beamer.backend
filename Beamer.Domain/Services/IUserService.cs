using Beamer.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(long id);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(long id, User user);
        Task<bool> DeleteUser(long id);
    }
}
