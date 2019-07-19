using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    interface IProjectController
    {
        Task<ActionResult<IEnumerable<Project>>> GetProjects();
        Task<ActionResult<Project>> GetProject(long id);
        Task<ActionResult<Project>> CreateProject(Project project);
        Task<IActionResult> UpdateProject(long id, Project project);
        Task<IActionResult> DeleteProject(long id);
    }
}
