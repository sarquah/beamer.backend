using Beamer.Domain.Models;
using Beamer.Infrastructure.Persistance.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Beamer.UnitTest.Repositories
{
	public class ProjectRepository_UnitTest : Repository_UnitTest
	{
		private ProjectRepository sut;
		private Project project;
		private IEnumerable<Project> projects;

		public ProjectRepository_UnitTest() : base() 
		{
			sut = new ProjectRepository(_context);
			project = new Project()
			{
				Name = "Test Integration Project",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is an integration test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid()
			};
			projects = new List<Project>()
			{
				project
			};
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_Projects_Returns_Projects()
		{
			// Arrange
			await sut.CreateProject(project);
			var expectedResult = JsonConvert.SerializeObject(projects);
			// Act
			var result = await sut.GetProjects(project.TenantId);
			var stringResult = JsonConvert.SerializeObject(result);
			// Assert
			Assert.Equal(expectedResult, stringResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_Project_Returns_Project()
		{
			// Arrange
			await sut.CreateProject(project);
			var getProjects = await sut.GetProjects(project.TenantId);
			project.Id = getProjects.First().Id;
			var expectedResult = JsonConvert.SerializeObject(project);
			// Act
			var result = await sut.GetProject(project.Id, project.TenantId);
			var stringResult = JsonConvert.SerializeObject(result);
			// Assert
			Assert.Equal(expectedResult, stringResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task Create_Project_Returns_True()
		{
			// Arrange
			var expectedResult = true;
			// Act
			var result = await sut.CreateProject(project);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public async System.Threading.Tasks.Task Update_Project_Returns_True()
		{
			// Arrange
			await sut.CreateProject(project);
			_context.Entry(project).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getProjects = await sut.GetProjects(project.TenantId);
			var id = getProjects.First().Id;
			var expectedResult = true;
			var updatedProject = new Project()
			{
				Id = id,
				Name = "Test Unit Project Update",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test update",
				Status = EStatus.NotStarted,
				TenantId = project.TenantId
			};
			// Act
			var result = await sut.UpdateProject(id, updatedProject);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public async System.Threading.Tasks.Task Delete_Project_Returns_True()
		{
			// Arrange		
			await sut.CreateProject(project);
			_context.Entry(project).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getProjects = await sut.GetProjects(project.TenantId);
			var id = getProjects.First().Id;
			var expectedResult = true;
			// Act
			var result = await sut.DeleteProject(id);
			// Assert
			Assert.Equal(expectedResult, result);
		}
	}
}
