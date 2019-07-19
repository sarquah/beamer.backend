using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;            
        }

        // GET: api/v1/task/tasks
        [HttpGet("tasks")]
        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            return await _taskService.GetTasks();            
        }

        // GET: api/v1/task/1
        [HttpGet("{id}")]
        public async Task<Models.Task> GetTask(long id)
        {
            return await _taskService.GetTask(id);            
        }

        // POST: api/v1/task
        [HttpPost]
        public void CreateTask(Models.Task task)
        {
            _taskService.CreateTask(task);            
        }

        // PUT: api/v1/task/1
        [HttpPut("{id}")]
        public void UpdateTask(long id, Models.Task task)
        {
            _taskService.UpdateTask(id, task);            
        }

        // DELETE: api/v1/task/1
        [HttpDelete("{id}")]
        public void DeleteTask(long id)
        {
            _taskService.DeleteTask(id);            
        }
    }
}
