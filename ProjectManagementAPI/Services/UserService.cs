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

        public void CreateUser(User user)
        {
            _userRepository.CreateUser(user);
        }

        public void DeleteUser(long id)
        {
            _userRepository.DeleteUser(id);
        }

        public async Task<User> GetUser(long id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public void UpdateUser(long id, User user)
        {
            _userRepository.UpdateUser(id, user);
        }
    }
}
