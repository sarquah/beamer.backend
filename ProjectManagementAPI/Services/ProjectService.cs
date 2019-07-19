using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Controllers;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateProject(Project project)
        {
            _projectRepository.CreateProject(project);
        }

        public void DeleteProject(long id)
        {
            _projectRepository.DeleteProject(id);
        }

        public async Task<Project> GetProject(long id)
        {
            return await _projectRepository.GetProject(id);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectRepository.GetProjects();
        }

        public void UpdateProject(long id, Project project)
        {
            _projectRepository.UpdateProject(id, project);
        }
    }
}
