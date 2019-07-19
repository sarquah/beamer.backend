﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<Models.Task>> GetTasks();
        Task<Models.Task> GetTask(long id);
        void CreateTask(Models.Task task);
        void UpdateTask(long id, Models.Task task);
        void DeleteTask(long id);
    }
}
