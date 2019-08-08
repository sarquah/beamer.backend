using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Infrastructure.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Persistance.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteUser(long id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return false;
            }
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
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

        public async Task<bool> UpdateUser(long id, User user)
        {
            var foundUser = await _context.Users.FindAsync(id);
            if (foundUser == null)
            {
                return false;
            }
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
