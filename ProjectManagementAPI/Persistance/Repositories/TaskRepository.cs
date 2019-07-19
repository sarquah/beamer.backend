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

        public void CreateTask(Models.Task task)
        {            
            _context.Tasks.Add(task);
            _context.SaveChangesAsync();
        }

        public void DeleteTask(long id)
        {
            var task = _context.Tasks.Find(id);
            _context.Tasks.Remove(task);
            _context.SaveChangesAsync();
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

        public void UpdateTask(long id, Models.Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChangesAsync();            
        }
    }
}
