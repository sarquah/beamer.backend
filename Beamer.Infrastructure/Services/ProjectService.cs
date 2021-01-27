using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Infrastructure.Services
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
        public async Task<Project> GetProject(long id, Guid tenantId) => await _projectRepository.GetProject(id, tenantId);
        public async Task<IEnumerable<Project>> GetProjects(Guid tenantId) => await _projectRepository.GetProjects(tenantId);
        public async Task<bool> UpdateProject(long id, Project project) => await _projectRepository.UpdateProject(id, project);
    }
}
