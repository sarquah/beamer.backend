using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Persistance.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChangesAsync();
        }

        public void DeleteUser(long id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public void UpdateUser(long id, User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChangesAsync();            
        }
    }
}
