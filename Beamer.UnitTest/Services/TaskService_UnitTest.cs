using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Beamer.UnitTest.Services
{
	public class TaskService_UnitTest
	{
		private TaskService sut;
		private Domain.Models.Task _task;
		private List<Domain.Models.Task> _tasks;

		public TaskService_UnitTest()
		{
			_task = new Domain.Models.Task()
			{
				Id = 1,
				Name = "Test Unit Task",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid(),
				ProjectId = 1,
				OwnerId = 1
			};
			_tasks = new List<Domain.Models.Task>()
			{
				_task
			};
			var mockTaskRepository = new Mock<ITaskRepository>();
			mockTaskRepository.Setup(repository => repository.GetTasks(It.IsAny<Guid>())).ReturnsAsync(_tasks);
			mockTaskRepository.Setup(repository => repository.GetTask(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(_task);
			mockTaskRepository.Setup(repository => repository.CreateTask(It.IsAny<Domain.Models.Task>())).ReturnsAsync(true);
			mockTaskRepository.Setup(repository => repository.UpdateTask(It.IsAny<long>(), It.IsAny<Domain.Models.Task>())).ReturnsAsync(true);
			mockTaskRepository.Setup(repository => repository.DeleteTask(It.IsAny<long>())).ReturnsAsync(true);
			sut = new TaskService(mockTaskRepository.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTasks_Returns_Tasks()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var expectedResult = _tasks;
			// Act
			var response = await sut.GetTasks(tenantId);
			// Assert
			Assert.Equal(expectedResult, response);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTask_Returns_Task()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var expectedResult = _task;
			var taskId = 1;
			// Act
			var response = await sut.GetTask(taskId, tenantId);
			// Assert
			Assert.Equal(expectedResult, response);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateTask_Returns_True()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var task = new Domain.Models.Task()
			{
				Id = 1,
				Name = "Test Unit Task",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test",
				Status = EStatus.NotStarted,
				TenantId = tenantId,
				ProjectId = 1,
				OwnerId = 1
			};
			var expectedResult = true;
			// Act
			var response = await sut.CreateTask(task);
			// Assert
			Assert.Equal(expectedResult, response);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateTask_Returns_True()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var task = new Domain.Models.Task()
			{
				Id = 1,
				Name = "Test Unit Task Update",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test update",
				Status = EStatus.NotStarted,
				TenantId = tenantId,
				ProjectId = 1,
				OwnerId = 1
			};
			var expectedResult = true;
			// Act
			var response = await sut.UpdateTask(task.Id, task);
			// Assert
			Assert.Equal(expectedResult, response);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteTask_Returns_True()
		{
			// Arrange
			var taskId = 1;
			var expectedResult = true;
			// Act
			var response = await sut.DeleteTask(taskId);
			// Assert
			Assert.Equal(expectedResult, response);
		}
	}
}
