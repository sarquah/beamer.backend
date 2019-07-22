using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementAPI.Persistance.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context){}

        public async Task CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(long id)
        {
            var project = _context.Projects.Find(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Project> GetProject(long id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.Include(p => p.Tasks).Include(p => p.Owner).ToListAsync();
        }

        public async Task UpdateProject(long id, Project project)
        {
            project.Id = id;
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
