using AutoMapper;
using Beamer.API.Controllers;
using Beamer.Domain.Models;
using Beamer.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System;
using Xunit;

namespace Beamer.UnitTest.Controllers
{
	public class ProjectController_UnitTest
	{
		private ProjectController _projectController;

		public ProjectController_UnitTest()
		{			
			var project = new Project()
			{
				Id = 1,
				Name = "Test Unit Project",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid()
			};
			var projects = new List<Project>()
			{ 
				project 
			};
			projects.Add(project);
			var mockProjectService = new Mock<IProjectService>();
			mockProjectService.Setup(service => service.GetProjects(It.IsAny<Guid>())).ReturnsAsync(projects);
			mockProjectService.Setup(service => service.GetProject(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(project);
			mockProjectService.Setup(service => service.CreateProject(It.IsAny<Project>())).ReturnsAsync(true);
			mockProjectService.Setup(service => service.UpdateProject(It.IsAny<long>(), It.IsAny<Project>())).ReturnsAsync(true);
			mockProjectService.Setup(service => service.DeleteProject(It.IsAny<long>())).ReturnsAsync(true);
			var mockMapper = new Mock<IMapper>();
			_projectController = new ProjectController(mockProjectService.Object, mockMapper.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetProjects_Returns_ActionResultOfProjects()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			// Act
			var response = await _projectController.GetProjects(tenantId);
			// Assert
			Assert.IsType<ActionResult<IEnumerable<ProjectDTO>>>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetProject_Returns_ActionResultOfProject()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var projectId = 1;
			// Act
			var response = await _projectController.GetProject(projectId, tenantId);
			// Assert
			Assert.IsType<ActionResult<ProjectDetailsDTO>>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateProject_Returns_CreatedAtActionResult()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var project = new Project()
			{
				Id = 1,
				Name = "Test Unit Project",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test",
				Status = EStatus.NotStarted,
				TenantId = tenantId
			};
			// Act
			var response = await _projectController.CreateProject(project);
			// Assert
			Assert.IsType<CreatedAtActionResult>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateProject_Returns_NoContentResult()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var project = new Project()
			{
				Id = 1,
				Name = "Test Unit Project Update",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test update",
				Status = EStatus.NotStarted,
				TenantId = tenantId
			};
			// Act
			var response = await _projectController.UpdateProject(project.Id, project);
			// Assert
			Assert.IsType<NoContentResult>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteProject_Returns_NoContentResult()
		{
			// Arrange
			var projectId = 1;
			// Act
			var response = await _projectController.DeleteProject(projectId);
			// Assert
			Assert.IsType<NoContentResult>(response);
		}
	}
}
