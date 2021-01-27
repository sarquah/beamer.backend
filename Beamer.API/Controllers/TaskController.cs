using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Beamer.Domain.Models;
using Beamer.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Beamer.API.Controllers
{
    [Route("api/v1/task")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        // GET: api/v1/task/tasks        
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks(Guid tenantId)
        {
            var tasks = await _taskService.GetTasks(tenantId);
            var tasksDTO = _mapper.Map<IEnumerable<TaskDTO>>(tasks);
            return Ok(tasksDTO);
        }

        // GET: api/v1/task/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetailsDTO>> GetTask(long id, Guid tenantId)
        {
            var task = await _taskService.GetTask(id, tenantId);
            if (task == null)
            {
                return NotFound();
            }
            var taskDetailsDTO = _mapper.Map<TaskDetailsDTO>(task);
            return taskDetailsDTO;
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
