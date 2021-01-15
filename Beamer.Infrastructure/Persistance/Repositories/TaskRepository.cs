using Microsoft.EntityFrameworkCore;
using Beamer.Domain.Repositories;
using Beamer.Infrastructure.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Infrastructure.Persistance.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CreateTask(Domain.Models.Task task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> DeleteTask(long id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return false;
            }
            try
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Domain.Models.Task> GetTask(long id)
        {
            var task = await _context.Tasks
                .AsNoTracking()
                .Include(t => t.Owner)
                .Include(t => t.Project)
                    .ThenInclude(p => p.Owner)
                    .FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public async Task<IEnumerable<Domain.Models.Task>> GetTasks()
        {
            return await _context.Tasks
                .AsNoTracking()
                .Include(t => t.Owner)
                .Include(t => t.Project)
                    .ThenInclude(p => p.Owner)
                .ToListAsync();
        }

        public async Task<bool> UpdateTask(long id, Domain.Models.Task task)
        {
            var foundTask = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            if (foundTask == null)
            {
                return false;
            }
            try
            {
                _context.Entry(task).State = EntityState.Modified;
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
