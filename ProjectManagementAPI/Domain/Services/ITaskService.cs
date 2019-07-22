﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<Models.Task>> GetTasks();
        Task<Models.Task> GetTask(long id);
        Task<bool> CreateTask(Models.Task task);
        Task<bool> UpdateTask(long id, Models.Task task);
        Task<bool> DeleteTask(long id);
    }
}
