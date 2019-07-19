using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Controllers;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task CreateProject(Project project)
        {
           await _projectRepository.CreateProject(project);
        }

        public async Task DeleteProject(long id)
        {
            await _projectRepository.DeleteProject(id);
        }

        public async Task<Project> GetProject(long id)
        {
            return await _projectRepository.GetProject(id);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectRepository.GetProjects();
        }

        public async Task UpdateProject(long id, Project project)
        {
            await _projectRepository.UpdateProject(id, project);
        }
    }
}
