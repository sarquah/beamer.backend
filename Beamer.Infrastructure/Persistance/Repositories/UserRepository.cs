using Microsoft.EntityFrameworkCore;
using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Infrastructure.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Beamer.Infrastructure.Persistance.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CreateUser(User user)
        {
            var userDB = _context.Users.Find(user.Id);
            if (userDB != null)
            {
                return false;
            }
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> CreateUsers(IEnumerable<User> users)
        {            
            var usersToDB = users.Except(_context.Users);
            try
            {
                _context.Users.AddRange(usersToDB);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
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
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<User> GetUser(long id, Guid tenantId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id && u.TenantId == tenantId);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(Guid tenantId)
        {
            return await _context.Users
                .Where(u => u.TenantId == tenantId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(long id, User user)
        {
            var foundUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
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
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
