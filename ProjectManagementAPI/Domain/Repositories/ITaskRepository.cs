﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetTasks();
        Task<Models.Task> GetTask(long id);
        Task CreateTask(Models.Task task);
        Task UpdateTask(long id, Models.Task task);
        Task DeleteTask(long id);
    }
}