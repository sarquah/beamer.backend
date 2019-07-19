using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(long id);
        void CreateProject(Project project);
        void UpdateProject(long id, Project project);
        void DeleteProject(long id);
    }
}
