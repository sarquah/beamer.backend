using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Domain.Repositories;
using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Persistance.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context){}

        public void CreateProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChangesAsync();
        }

        public void DeleteProject(long id)
        {
            var project = _context.Projects.Find(id);
            _context.Projects.Remove(project);
            _context.SaveChangesAsync();
        }

        public Task<Project> GetProject(long id)
        {
            var project = _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.Include(p => p.Tasks).Include(p => p.Owner).ToListAsync();
        }

        public void UpdateProject(long id, Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}
