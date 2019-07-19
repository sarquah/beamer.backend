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

        public void CreateTask(Models.Task task)
        {
            _taskRepository.CreateTask(task);
        }

        public void DeleteTask(long id)
        {
            _taskRepository.DeleteTask(id);
        }

        public async Task<Models.Task> GetTask(long id)
        {
            return await _taskRepository.GetTask(id);
        }

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            return await _taskRepository.GetTasks();
        }

        public void UpdateTask(long id, Models.Task task)
        {
            _taskRepository.UpdateTask(id, task);
        }
    }
}
