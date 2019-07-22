using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementAPI.Persistance.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task CreateTask(Models.Task task)
        {            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(long id)
        {
            var task = _context.Tasks.Find(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Task> GetTask(long id)
        {
            var task = await _context.Tasks.Include(t => t.Owner).Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            return await _context.Tasks.Include(t => t.Owner).Include(t => t.Project).ToListAsync();
        }

        public async Task UpdateTask(long id, Models.Task task)
        {
            task.Id = id;
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
