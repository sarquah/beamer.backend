using Beamer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers(Guid tenantId);
        Task<User> GetUser(long id, Guid tenantId);
        Task<bool> CreateUser(User user);
        Task<bool> CreateUsers(IEnumerable<User> users);
        Task<bool> UpdateUser(long id, User user);
        Task<bool> DeleteUser(long id);
    }
}
