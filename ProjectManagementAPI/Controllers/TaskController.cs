using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.API.Controllers
{
    [Route("api/v1/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/v1/task/tasks
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<Domain.Models.Task>>> GetTasks()
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }

        // GET: api/v1/task/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Models.Task>> GetTask(long id)
        {
            var task = await _taskService.GetTask(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // POST: api/v1/task
        [HttpPost]
        public async Task<ActionResult> CreateTask(Domain.Models.Task task)
        {
            bool success = await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/v1/task/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(long id, Domain.Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }
            bool success = await _taskService.UpdateTask(id, task);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/v1/task/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(long id)
        {
            bool success = await _taskService.DeleteTask(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
