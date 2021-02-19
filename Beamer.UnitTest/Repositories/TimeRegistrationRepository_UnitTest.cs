using Beamer.Domain.Models;
using Beamer.Infrastructure.Persistance.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Beamer.UnitTest.Repositories
{
	public class TimeRegistrationRepository_UnitTest : Repository_UnitTest
	{
		private TimeRegistrationRepository sut;
		private TimeRegistration timeRegistration;
		private List<TimeRegistration> timeRegistrations;

		public TimeRegistrationRepository_UnitTest() : base()
		{
			timeRegistration = new TimeRegistration()
			{
				StartDate = new DateTime(2021, 2, 19, 9, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 12, 0, 0),
				TenantId = Guid.NewGuid(),
				TaskId = 1
			};
			timeRegistrations = new List<TimeRegistration>()
			{
				timeRegistration
			};
			sut = new TimeRegistrationRepository(_context);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistration_When_Called_With_IdAndGuid_Returns_TimeRegistration()
		{
			// Arrange
			await sut.CreateTimeRegistration(timeRegistration, timeRegistration.TenantId);
			var expectedResult = JsonConvert.SerializeObject(timeRegistration);
			// Act
			var call = await sut.GetTimeRegistration(timeRegistration.Id, timeRegistration.TenantId);
			var actualResult = JsonConvert.SerializeObject(call);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistrationsForTask_When_Called_With_TaskIdAndGuid_Returns_TimeRegistrations()
		{
			// Arrange
			await sut.CreateTimeRegistration(timeRegistration, timeRegistration.TenantId);
			var expectedResult = JsonConvert.SerializeObject(timeRegistrations);
			// Act
			var call = await sut.GetTimeRegistrationsForTask(timeRegistration.TaskId.Value, timeRegistration.TenantId);
			var actualResult = JsonConvert.SerializeObject(call);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateTimeRegistration_When_Called_With_TimeRegistrationAndGuid_Returns_True()
		{
			// Arrange
			var expectedResult = true;
			// Act
			var actualResult = await sut.CreateTimeRegistration(timeRegistration, timeRegistration.TenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateTimeRegistration_When_Called_With_TimeRegistrationAndGuid_Returns_True()
		{
			// Arrange
			await sut.CreateTimeRegistration(timeRegistration, timeRegistration.TenantId);
			_context.Entry(timeRegistration).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getTimeRegistrations = await sut.GetTimeRegistrationsForTask(timeRegistration.TaskId.Value, timeRegistration.TenantId);
			var id = getTimeRegistrations.First().Id;
			var updatedTimeRegistration = new TimeRegistration()
			{
				Id = id,
				StartDate = new DateTime(2021, 2, 19, 12, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 14, 0, 0),
				OwnerId = 1337,
				TaskId = 1337,
				TenantId = timeRegistration.TenantId
			};
			var expectedResult = true;
			// Act
			var actualResult = await sut.UpdateTimeRegistration(id, updatedTimeRegistration, timeRegistration.TenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteTimeRegistration_When_Called_With_IdAndGuid_Returns_True()
		{
			// Arrange
			await sut.CreateTimeRegistration(timeRegistration, timeRegistration.TenantId);
			_context.Entry(timeRegistration).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getTimeRegistrations = await sut.GetTimeRegistrationsForTask(timeRegistration.TaskId.Value, timeRegistration.TenantId);
			var id = getTimeRegistrations.First().Id;
			var expectedResult = true;
			// Act
			var actualResult = await sut.DeleteTimeRegistration(id, timeRegistration.TenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}
	}
}
