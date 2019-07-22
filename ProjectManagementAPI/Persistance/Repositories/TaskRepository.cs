using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Persistance.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CreateTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTask(long id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Models.Task> GetTask(long id)
        {
            var task = await _context.Tasks
                .Include(t => t.Owner)
                .Include(t => t.Project)
                    .ThenInclude(t => t.Owner)
                    .FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            return await _context.Tasks
                .Include(t => t.Owner)
                .Include(t => t.Project)
                    .ThenInclude(t => t.Owner)
                .ToListAsync();
        }

        public async Task<bool> UpdateTask(long id, Models.Task task)
        {
            var foundTask = await _context.Tasks.FindAsync(id);
            if (foundTask == null)
            {
                return false;
            }
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
