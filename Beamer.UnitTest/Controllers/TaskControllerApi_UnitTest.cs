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
	public class TaskControllerApi_UnitTest
	{
		private TaskController _taskController;

		public TaskControllerApi_UnitTest()
		{
			var task = new Domain.Models.Task()
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
			var tasks = new List<Domain.Models.Task>()
			{
				task
			};
			tasks.Add(task);
			var mockTaskService = new Mock<ITaskService>();
			mockTaskService.Setup(service => service.GetTasks(It.IsAny<Guid>())).ReturnsAsync(tasks);
			mockTaskService.Setup(service => service.GetTask(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(task);
			mockTaskService.Setup(service => service.CreateTask(It.IsAny<Domain.Models.Task>())).ReturnsAsync(true);
			mockTaskService.Setup(service => service.UpdateTask(It.IsAny<long>(), It.IsAny<Domain.Models.Task>())).ReturnsAsync(true);
			mockTaskService.Setup(service => service.DeleteTask(It.IsAny<long>())).ReturnsAsync(true);
			var mockMapper = new Mock<IMapper>();
			_taskController = new TaskController(mockTaskService.Object, mockMapper.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTasks_Returns_ActionResultOfTasks()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			// Act
			var response = await _taskController.GetTasks(tenantId);
			// Assert
			Assert.IsAssignableFrom<ActionResult<IEnumerable<TaskDTO>>>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTask_Returns_ActionResultOfTask()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var taskId = 1;
			// Act
			var response = await _taskController.GetTask(taskId, tenantId);
			// Assert
			Assert.IsAssignableFrom<ActionResult<TaskDetailsDTO>>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateTask_Returns_CreatedAtActionResult()
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
			// Act
			var response = await _taskController.CreateTask(task);
			// Assert
			Assert.IsAssignableFrom<CreatedAtActionResult>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateTask_Returns_NoContentResult()
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
			// Act
			var response = await _taskController.UpdateTask(task.Id, task);
			// Assert
			Assert.IsAssignableFrom<NoContentResult>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteTask_Returns_NoContentResult()
		{
			// Arrange
			var taskId = 1;
			// Act
			var response = await _taskController.DeleteTask(taskId);
			// Assert
			Assert.IsAssignableFrom<NoContentResult>(response);
		}
	}
}
