using Beamer.Domain.Models;
using Beamer.Infrastructure.Persistance.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Beamer.UnitTest.Repositories
{
	public class TaskRepository_UnitTest : Repository_UnitTest
	{
		private TaskRepository sut;
		private Task task;
		private IEnumerable<Task> tasks;

		public TaskRepository_UnitTest() : base()
		{

			sut = new TaskRepository(_context);
			task = new Task()
			{
				Name = "Test Unit Task",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid()
			};
			tasks = new List<Task>()
			{
				task
			};
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_Tasks_Returns_Tasks()
		{
			// Arrange
			await sut.CreateTask(task);
			var expectedResult = JsonConvert.SerializeObject(tasks);
			// Act
			var result = await sut.GetTasks(task.TenantId);
			var stringResult = JsonConvert.SerializeObject(result);
			// Assert
			Assert.Equal(expectedResult, stringResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_Task_Returns_Task()
		{
			// Arrange
			await sut.CreateTask(task);
			var getTasks = await sut.GetTasks(task.TenantId);
			task.Id = getTasks.First().Id;
			var expectedResult = JsonConvert.SerializeObject(task);
			// Act
			var result = await sut.GetTask(task.Id, task.TenantId);
			var stringResult = JsonConvert.SerializeObject(result);
			// Assert
			Assert.Equal(expectedResult, stringResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task Create_Task_Returns_True()
		{
			// Arrange
			var expectedResult = true;
			// Act
			var result = await sut.CreateTask(task);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public async System.Threading.Tasks.Task Update_Task_Returns_True()
		{
			// Arrange
			await sut.CreateTask(task);
			_context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getTasks = await sut.GetTasks(task.TenantId);
			var id = getTasks.First().Id;
			var expectedResult = true;
			var updatedTask = new Task()
			{
				Id = id,
				Name = "Test Unit Task Update",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is a unit test update",
				Status = EStatus.NotStarted,
				TenantId = task.TenantId
			};
			// Act
			var result = await sut.UpdateTask(id, updatedTask);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public async System.Threading.Tasks.Task Delete_Task_Returns_True()
		{
			// Arrange		
			await sut.CreateTask(task);
			_context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getTasks = await sut.GetTasks(task.TenantId);
			var id = getTasks.First().Id;
			var expectedResult = true;
			// Act
			var result = await sut.DeleteTask(id);
			// Assert
			Assert.Equal(expectedResult, result);
		}
	}
}
