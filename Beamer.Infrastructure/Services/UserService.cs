using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(User user) => await _userRepository.CreateUser(user);
        public async Task<bool> CreateUsers(IEnumerable<User> users) => await _userRepository.CreateUsers(users);
        public async Task<bool> DeleteUser(long id) => await _userRepository.DeleteUser(id);
        public async Task<User> GetUser(long id, Guid tenantId) => await _userRepository.GetUser(id, tenantId);
        public async Task<IEnumerable<User>> GetUsers(Guid tenantId) => await _userRepository.GetUsers(tenantId);
        public async Task<bool> UpdateUser(long id, User user) => await _userRepository.UpdateUser(id, user);
    }
}
