using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> CreateProject(Project project) => await _projectRepository.CreateProject(project);
        public async Task<bool> DeleteProject(long id) => await _projectRepository.DeleteProject(id);
        public async Task<Project> GetProject(long id) => await _projectRepository.GetProject(id);
        public async Task<IEnumerable<Project>> GetProjects() => await _projectRepository.GetProjects();
        public async Task<bool> UpdateProject(long id, Project project) => await _projectRepository.UpdateProject(id, project);
    }
}
