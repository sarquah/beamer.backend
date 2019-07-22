using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
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
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }

        // GET: api/v1/task/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(long id)
        {
            return await _taskService.GetTask(id);            
        }

        // POST: api/v1/task
        [HttpPost]
        public async Task<ActionResult> CreateTask(Models.Task task)
        {
            await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/v1/task/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(long id, Models.Task task)
        {
            await _taskService.UpdateTask(id, task);
            return NoContent();
        }

        // DELETE: api/v1/task/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(long id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
