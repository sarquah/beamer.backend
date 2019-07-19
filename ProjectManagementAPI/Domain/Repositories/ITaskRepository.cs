using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetTasks();
        Task<Models.Task> GetTask(long id);
        void CreateTask(Models.Task task);
        void UpdateTask(long id, Models.Task task);
        void DeleteTask(long id);
    }
}
