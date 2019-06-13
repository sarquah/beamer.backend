using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/v1/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ProjectController(ProjectContext context)
        {
            _context = context;

            if (_context.Projects.Count()==0)
            {
                _context.Projects.Add(new Project { Name = "Empty project" });
                _context.SaveChanges();
            }
        }

        // GET: api/v1/project/projects
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/v1/project/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return project;
        }

        // POST: api/v1/project
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT: api/v1/project/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(long id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/v1/project/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
