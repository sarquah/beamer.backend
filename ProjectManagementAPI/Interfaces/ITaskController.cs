using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Interfaces
{
    interface ITaskController
    {
        Task<ActionResult<IEnumerable<Models.Task>>> GetTasks();
        Task<ActionResult<Models.Task>> GetTask(long id);
        Task<ActionResult<Models.Task>> CreateTask(Models.Task task);
        Task<IActionResult> UpdateTask(long id, Models.Task task);
        Task<IActionResult> DeleteTask(long id);
    }
}
