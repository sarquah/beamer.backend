using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetTasks(Guid tenantId);
        Task<Models.Task> GetTask(long id, Guid tenantId);
        Task<bool> CreateTask(Models.Task task);
        Task<bool> UpdateTask(long id, Models.Task task);
        Task<bool> DeleteTask(long id);
    }
}
