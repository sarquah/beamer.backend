using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(User user) => await _userRepository.CreateUser(user);
        public async Task<bool> DeleteUser(long id) => await _userRepository.DeleteUser(id);
        public async Task<User> GetUser(long id) => await _userRepository.GetUser(id);
        public async Task<IEnumerable<User>> GetUsers() => await _userRepository.GetUsers();
        public async Task<bool> UpdateUser(long id, User user) => await _userRepository.UpdateUser(id, user);
    }
}
