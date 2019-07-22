using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Interfaces;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
           await _userRepository.CreateUser(user);
        }

        public async Task DeleteUser(long id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<User> GetUser(long id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task UpdateUser(long id, User user)
        {
            await _userRepository.UpdateUser(id, user);
        }
    }
}
