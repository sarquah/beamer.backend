using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(long id);
        void CreateUser(User user);
        void UpdateUser(long id, User user);
        void DeleteUser(long id);
    }
}
