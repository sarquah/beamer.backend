using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Infrastructure.Services;
using Moq;
using System.Collections.Generic;
using System;
using Xunit;

namespace Beamer.UnitTest.Services
{
	public class ProjectService_UnitTest
	{
		private ProjectService _projectService;
		private Project _project;
		private List<Project> _projects;

		public ProjectService_UnitTest()
		{
			_project = new Project()
			{
				Id = 1,
				Name = "Test Unit Project",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid()
			};
			_projects = new List<Project>()
			{
				_project
			};
			var mockProjectRepository = new Mock<IProjectRepository>();
			mockProjectRepository.Setup(repository => repository.GetProjects(It.IsAny<Guid>())).ReturnsAsync(_projects);
			mockProjectRepository.Setup(repository => repository.GetProject(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(_project);
			mockProjectRepository.Setup(repository => repository.CreateProject(It.IsAny<Project>())).ReturnsAsync(true);
			mockProjectRepository.Setup(repository => repository.UpdateProject(It.IsAny<long>(), It.IsAny<Project>())).ReturnsAsync(true);
			mockProjectRepository.Setup(repository => repository.DeleteProject(It.IsAny<long>())).ReturnsAsync(true);

			_projectService = new ProjectService(mockProjectRepository.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetProjects_Returns_Projects()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var expectedResult = _projects;
			// Act
			var response = await _projectService.GetProjects(tenantId);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetProject_Returns_Project()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var projectId = 1;
			var expectedResult = _project;
			// Act
			var response = await _projectService.GetProject(projectId, tenantId);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateProject_Returns_True()
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
			var expectedResult = true;
			// Act
			var response = await _projectService.CreateProject(project);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateProject_Returns_True()
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
			var expectedResult = true;
			// Act
			var response = await _projectService.UpdateProject(project.Id, project);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteProject_Returns_True()
		{
			// Arrange
			var projectId = 1;
			var expectedResult = true;
			// Act
			var response = await _projectService.DeleteProject(projectId);
			// Assert
			Assert.Equal(response, expectedResult);
		}
	}
}
