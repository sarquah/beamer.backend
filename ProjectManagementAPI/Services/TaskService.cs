using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> CreateTask(Models.Task task) => await _taskRepository.CreateTask(task);
        public async Task<bool> DeleteTask(long id) => await _taskRepository.DeleteTask(id);
        public async Task<Models.Task> GetTask(long id) => await _taskRepository.GetTask(id);
        public async Task<IEnumerable<Models.Task>> GetTasks() => await _taskRepository.GetTasks();
        public async Task<bool> UpdateTask(long id, Models.Task task) => await _taskRepository.UpdateTask(id, task);
    }
}
