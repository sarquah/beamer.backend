using Beamer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects(Guid tenantId);
        Task<Project> GetProject(long id, Guid tenantId);
        Task<bool> CreateProject(Project project);
        Task<bool> UpdateProject(long id, Project project);
        Task<bool> DeleteProject(long id);
    }
}
