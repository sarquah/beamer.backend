using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
            if (_context.Tasks.Count() == 0)
            {
                _context.Tasks.Add(new Models.Task { Name = "Empty task" });
                _context.SaveChanges();
            }
        }

        // GET: api/v1/task/tasks
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/v1/task/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(long id)
        {
            var task = await _context.Tasks.FindAsync(id);
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
