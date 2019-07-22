using ProjectManagementAPI.Controllers;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> CreateProject(Project project)
        {
           return await _projectRepository.CreateProject(project);
        }

        public async Task<bool> DeleteProject(long id)
        {
            return await _projectRepository.DeleteProject(id);
        }

        public async Task<Project> GetProject(long id)
        {
            return await _projectRepository.GetProject(id);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectRepository.GetProjects();
        }

        public async Task<bool> UpdateProject(long id, Project project)
        {
            return await _projectRepository.UpdateProject(id, project);
        }
    }
}
