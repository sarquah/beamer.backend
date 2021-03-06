﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Beamer.Domain.Models;
using Beamer.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Beamer.API.Controllers
{
    [Route("api/v1/project")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        // GET: api/v1/project/projects
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects(Guid tenantId)
        {
            var projects = await _projectService.GetProjects(tenantId);
            var projectsDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);
            return Ok(projectsDTO);
        }

        // GET: api/v1/project/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailsDTO>> GetProject(long id, Guid tenantId)
        {
            var project = await _projectService.GetProject(id, tenantId);
            if (project == null)
            {
                return NotFound();
            }
            var projectDetailsDTO = _mapper.Map<ProjectDetailsDTO>(project);
            return projectDetailsDTO;
        }

        // POST: api/v1/project
        [HttpPost]
        public async Task<ActionResult> CreateProject(Project project)
        {
            bool success = await _projectService.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT: api/v1/project/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(long id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }
            bool success = await _projectService.UpdateProject(id, project);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/v1/project/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(long id)
        {
            bool success = await _projectService.DeleteProject(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
