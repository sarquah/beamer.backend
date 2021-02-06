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
	public class UserController_UnitTest
	{
		private UserController sut;

		public UserController_UnitTest()
		{
			var user = new User()
			{
				Id = 1,
				Name = "Test Person",
				Department = "Development",
				Email = "test@test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			var users = new List<User>()
			{
				user
			};
			var mockUserService = new Mock<IUserService>();
			mockUserService.Setup(service => service.GetUsers(It.IsAny<Guid>())).ReturnsAsync(users);
			mockUserService.Setup(service => service.GetUser(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(user);
			mockUserService.Setup(service => service.CreateUser(It.IsAny<User>())).ReturnsAsync(true);
			mockUserService.Setup(service => service.CreateUsers(It.IsAny<IEnumerable<User>>())).ReturnsAsync(true);
			mockUserService.Setup(service => service.UpdateUser(It.IsAny<long>(), It.IsAny<Domain.Models.User>())).ReturnsAsync(true);
			mockUserService.Setup(service => service.DeleteUser(It.IsAny<long>())).ReturnsAsync(true);
			var mockMapper = new Mock<IMapper>();
			sut = new UserController(mockUserService.Object, mockMapper.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetUsers_Returns_ActionResultOfUsers()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			// Act
			var response = await sut.GetUsers(tenantId);
			// Assert
			Assert.IsType<ActionResult<IEnumerable<UserDTO>>>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetUser_Returns_ActionResultOfUser()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var userId = 1;
			// Act
			var response = await sut.GetUser(userId, tenantId);
			// Assert
			Assert.IsType<ActionResult<UserDetailsDTO>>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateUser_Returns_CreatedAtActionResult()
		{
			// Arrange
			var user = new User()
			{
				Id = 1,
				Name = "Test Person",
				Department = "Development",
				Email = "test@test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			// Act
			var response = await sut.CreateUser(user);
			// Assert
			Assert.IsType<CreatedAtActionResult>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateUsers_Returns_CreatedAtActionResult()
		{
			// Arrange
			var user = new User()
			{
				Id = 1,
				Name = "Test Person",
				Department = "Development",
				Email = "test@test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			var users = new List<User>()
			{
				user
			};
			// Act
			var response = await sut.CreateUsers(users);
			// Assert
			Assert.IsType<CreatedAtActionResult>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateUser_Returns_OkResult()
		{
			// Arrange
			var user = new User()
			{
				Id = 1,
				Name = "Test Person Update",
				Department = "Test Department",
				Email = "test@test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			// Act
			var response = await sut.UpdateUser(user.Id, user);
			// Assert
			Assert.IsType<OkResult>(response);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteUser_Returns_OkResult()
		{
			// Arrange
			var userId = 1;
			// Act
			var response = await sut.DeleteUser(userId);
			// Assert
			Assert.IsType<OkResult>(response);
		}
	}
}
