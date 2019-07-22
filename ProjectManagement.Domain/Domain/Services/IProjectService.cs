using ProjectManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(long id);
        Task<bool> CreateProject(Project project);
        Task<bool> UpdateProject(long id, Project project);
        Task<bool> DeleteProject(long id);
    }
}
