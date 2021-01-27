using Microsoft.EntityFrameworkCore;
using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Infrastructure.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Beamer.Infrastructure.Persistance.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CreateProject(Project project)
        {
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return false;
            }
            try
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Project> GetProject(long id, Guid tenantId)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Owner)
                .Include(p => p.Owner)            
                .FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId);
            return project;
        }

        public async Task<IEnumerable<Project>> GetProjects(Guid tenantId)
        {
            return await _context.Projects
                .AsNoTracking()
                .Where(p => p.TenantId == tenantId)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Owner)
                .Include(p => p.Owner)            
                .ToListAsync();
        }

        public async Task<bool> UpdateProject(long id, Project project)
        {
            var foundProject = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (foundProject == null)
            {
                return false;
            }
            try
            {
                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
