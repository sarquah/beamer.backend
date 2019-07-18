using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        public TaskController(ProjectManagementContext context)
        {
            _context = context;            
        }

        // GET: api/v1/task/tasks
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return await _context.Tasks.Include(t => t.Owner).Include(t => t.Project).ToListAsync();
        }

        // GET: api/v1/task/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(long id)
        {
            var task = await _context.Tasks.Include(t => t.Owner).Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // POST: api/v1/task
        [HttpPost]
        public async Task<ActionResult<Models.Task>> CreateTask(Models.Task task)
        {
            var project = await _context.Projects.FindAsync(task.ProjectId);
            if (project == null)
            {
                return NotFound();
            }
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/v1/task/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(long id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/v1/task/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(long id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
