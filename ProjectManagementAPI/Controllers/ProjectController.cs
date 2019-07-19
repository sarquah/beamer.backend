using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/project")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/v1/project/projects
        [HttpGet("projects")]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectService.GetProjects();                
        }

        // GET: api/v1/project/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(long id)
        {
            return await _projectService.GetProject(id);                
        }

        // POST: api/v1/project
        [HttpPost]
        public void CreateProject(Project project)
        {
            _projectService.CreateProject(project);            
        }

        // PUT: api/v1/project/1
        [HttpPut("{id}")]
        public void UpdateProject(long id, Project project)
        {
            _projectService.UpdateProject(id, project);            
        }

        // DELETE: api/v1/project/1
        [HttpDelete("{id}")]
        public void DeleteProject(long id)
        {
            _projectService.DeleteProject(id);            
        }
    }
}
