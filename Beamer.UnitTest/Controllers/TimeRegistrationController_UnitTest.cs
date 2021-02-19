using AutoMapper;
using Beamer.API.Controllers;
using Beamer.Domain.Models;
using Beamer.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Beamer.UnitTest.Controllers
{
	public class TimeRegistrationController_UnitTest
	{
		private TimeRegistrationController sut;

		public TimeRegistrationController_UnitTest()
		{
			var timeRegistration = new TimeRegistration()
			{
				Id = 1,
				StartDate = new DateTime(2021, 2, 19, 9, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 12, 0, 0),
				TenantId = Guid.NewGuid(),
				TaskId = 1,
				OwnerId = 1
			};
			var timeRegistrations = new List<TimeRegistration>()
			{
				timeRegistration
			};
			var mockTimeRegistrationService = new Mock<ITimeRegistrationService>();
			mockTimeRegistrationService.Setup(service => service.GetTimeRegistration(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(timeRegistration);
			mockTimeRegistrationService.Setup(service => service.GetTimeRegistrationsForTask(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(timeRegistrations);
			mockTimeRegistrationService.Setup(service => service.CreateTimeRegistration(It.IsAny<TimeRegistration>(), It.IsAny<Guid>())).ReturnsAsync(true);
			mockTimeRegistrationService.Setup(service => service.UpdateTimeRegistration(It.IsAny<long>(), It.IsAny<TimeRegistration>(), It.IsAny<Guid>())).ReturnsAsync(true);
			mockTimeRegistrationService.Setup(service => service.DeleteTimeRegistration(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(true);
			var mockMapper = new Mock<IMapper>();
			sut = new TimeRegistrationController(mockTimeRegistrationService.Object, mockMapper.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistration_When_Called_With_IdAndGuid_Returns_ActionResult()
		{
			// Arrange
			var id = 1;
			var tenantId = Guid.NewGuid();
			// Act
			var actualResult = await sut.GetTimeRegistration(id, tenantId);
			// Assert
			Assert.IsType<ActionResult<TimeRegistration>>(actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistrationsForTask_When_Called_With_TaskIdAndGuid_Returns_ActionResult()
		{
			// Arrange
			var taskId = 1;
			var tenantId = Guid.NewGuid();
			// Act
			var actualResult = await sut.GetTimeRegistrationsForTask(taskId, tenantId);
			// Assert
			Assert.IsType<ActionResult<IEnumerable<TimeRegistration>>>(actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateTimeRegistration_When_Called_With_TimeRegistrationAndGuid_Returns_CreatedAtActionResult()
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
			// Act
			var actualResult = await sut.CreateTimeRegistration(timeRegistration, tenantId);
			// Assert
			Assert.IsType<CreatedAtActionResult>(actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateTimeRegistration_When_Called_With_TimeRegistrationAndGuid_Returns_NoContentResult()
		{
			// Arrange
			var id = 1;
			var timeRegistration = new TimeRegistration()
			{
				Id = 1,
				StartDate = new DateTime(2021, 2, 19, 12, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 14, 0, 0),
				OwnerId = 1337,
				TaskId = 1337,
				TenantId = Guid.NewGuid()
			};
			var tenantId = Guid.NewGuid();
			// Act
			var actualResult = await sut.UpdateTimeRegistration(id, timeRegistration, tenantId);
			// Assert
			Assert.IsType<NoContentResult>(actualResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteTimeRegistration_When_Called_With_IdAndGuid_Returns_NoContentResult()
		{
			// Arrange
			var id = 1;
			var tenantId = Guid.NewGuid();
			// Act
			var actualResult = await sut.DeleteTimeRegistration(id, tenantId);
			// Assert
			Assert.IsType<NoContentResult>(actualResult);
		}
	}
}
