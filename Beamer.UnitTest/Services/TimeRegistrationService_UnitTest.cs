using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Beamer.UnitTest.Services
{
	public class TimeRegistrationService_UnitTest
	{
		private TimeRegistrationService sut;
		private TimeRegistration timeRegistration;
		private List<TimeRegistration> timeRegistrations;

		public TimeRegistrationService_UnitTest()
		{
			timeRegistration = new TimeRegistration()
			{
				Id = 1,
				StartDate = new DateTime(2021, 2, 19, 9, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 12, 0, 0),
				TenantId = Guid.NewGuid(),
				TaskId = 1,
				OwnerId = 1
			};
			timeRegistrations = new List<TimeRegistration>()
			{
				timeRegistration
			};
			var mockTimeRegistrationRepository = new Mock<ITimeRegistrationRepository>();
			mockTimeRegistrationRepository.Setup(repository => repository.CreateTimeRegistration(It.IsAny<TimeRegistration>(), It.IsAny<Guid>())).ReturnsAsync(true);
			mockTimeRegistrationRepository.Setup(repository => repository.DeleteTimeRegistration(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(true);
			mockTimeRegistrationRepository.Setup(repository => repository.GetTimeRegistration(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(timeRegistration);
			mockTimeRegistrationRepository.Setup(repository => repository.GetTimeRegistrationsForTask(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(timeRegistrations);
			mockTimeRegistrationRepository.Setup(repository => repository.UpdateTimeRegistration(It.IsAny<long>(), It.IsAny<TimeRegistration>(), It.IsAny<Guid>())).ReturnsAsync(true);
			sut = new TimeRegistrationService(mockTimeRegistrationRepository.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistration_When_Called_With_IdAndGuid_Returns_TimeRegistration()
		{
			// Arrange
			var id = 1;
			var tenantId = Guid.NewGuid();
			var expectedResult = timeRegistration;
			// Act
			var actualResult = await sut.GetTimeRegistration(id, tenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistrationsForTask_When_Called_With_TaskIdAndGuid_Returns_TimeRegistrations()
		{
			// Arrange
			var taskId = 1;
			var tenantId = Guid.NewGuid();
			var expectedResult = timeRegistrations;
			// Act
			var actualResult = await sut.GetTimeRegistrationsForTask(taskId, tenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateTimeRegistration_When_Called_With_TimeRegistrationAndGuid_Returns_True()
		{
			// Arrange
			var timeRegistration = new TimeRegistration()
			{
				Id = 1337,
				StartDate = new DateTime(2021, 2, 19, 9, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 14, 0, 0),
				OwnerId = 1337,
				TaskId = 1337,
				TenantId = Guid.NewGuid()
			};
			var tenantId = Guid.NewGuid();
			var expectedResult = true;
			// Act
			var actualResult = await sut.CreateTimeRegistration(timeRegistration, tenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateTimeRegistration_When_Called_With_TimeRegistrationAndGuid_Returns_True()
		{
			// Arrange
			var id = 1;
			var tenantId = Guid.NewGuid();
			var timeRegistration = new TimeRegistration()
			{
				Id = 1,
				StartDate = new DateTime(2021, 2, 19, 12, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 14, 0, 0),
				OwnerId = 1337,
				TaskId = 1337,
				TenantId = tenantId
			};			
			var expectedResult = true;
			// Act
			var actualResult = await sut.UpdateTimeRegistration(id, timeRegistration, tenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteTimeRegistration_When_Called_With_IdAndGuid_Returns_True()
		{
			// Arrange
			var id = 1;
			var tenantId = Guid.NewGuid();
			var expectedResult = true;
			// Act
			var actualResult = await sut.DeleteTimeRegistration(id, tenantId);
			// Assert
			Assert.Equal(expectedResult, actualResult);
		}
	}
}
