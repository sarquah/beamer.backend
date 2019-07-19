using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/v1/project/projects
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectService.GetProjects();
            return Ok(projects);
        }

        // GET: api/v1/project/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(long id)
        {
            return await _projectService.GetProject(id);                
        }

        // POST: api/v1/project
        [HttpPost]
        public async Task<ActionResult> CreateProject(Project project)
        {
            await _projectService.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT: api/v1/project/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(long id, Project project)
        {
            await _projectService.UpdateProject(id, project);
            return NoContent();
        }

        // DELETE: api/v1/project/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(long id)
        {
            await _projectService.DeleteProject(id);
            return NoContent();
        }
    }
}
