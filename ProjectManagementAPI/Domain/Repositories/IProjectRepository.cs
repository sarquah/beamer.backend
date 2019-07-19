using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(long id);
        void CreateProject(Project project);
        void UpdateProject(long id, Project project);
        void DeleteProject(long id);
    }
}
