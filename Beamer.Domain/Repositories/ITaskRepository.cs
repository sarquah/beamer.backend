using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetTasks();
        Task<Models.Task> GetTask(long id);
        Task<bool> CreateTask(Models.Task task);
        Task<bool> UpdateTask(long id, Models.Task task);
        Task<bool> DeleteTask(long id);
    }
}
