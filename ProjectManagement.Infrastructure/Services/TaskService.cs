using ProjectManagement.Domain.Repositories;
using ProjectManagement.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> CreateTask(Domain.Models.Task task) => await _taskRepository.CreateTask(task);
        public async Task<bool> DeleteTask(long id) => await _taskRepository.DeleteTask(id);
        public async Task<Domain.Models.Task> GetTask(long id) => await _taskRepository.GetTask(id);
        public async Task<IEnumerable<Domain.Models.Task>> GetTasks() => await _taskRepository.GetTasks();
        public async Task<bool> UpdateTask(long id, Domain.Models.Task task) => await _taskRepository.UpdateTask(id, task);
    }
}
