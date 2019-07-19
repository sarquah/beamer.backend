﻿using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementAPI.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(long id);
        Task CreateProject(Project project);
        Task UpdateProject(long id, Project project);
        Task DeleteProject(long id);
    }
}
