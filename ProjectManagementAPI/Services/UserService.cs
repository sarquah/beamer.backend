using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Interfaces;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<bool> DeleteUser(long id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<User> GetUser(long id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<bool> UpdateUser(long id, User user)
        {
            return await _userRepository.UpdateUser(id, user);
        }
    }
}
