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
	public class UserService_UnitTest
	{
		private UserService _userService;
		private User _user;
		private List<User> _users;

		public UserService_UnitTest()
		{
			_user = new User()
			{
				Id = 1,
				Name = "Test Person",
				Department = "Development",
				Email = "test@test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			_users = new List<User>()
			{
				_user
			};
			var mockUserRepository = new Mock<IUserRepository>();
			mockUserRepository.Setup(service => service.GetUsers(It.IsAny<Guid>())).ReturnsAsync(_users);
			mockUserRepository.Setup(service => service.GetUser(It.IsAny<long>(), It.IsAny<Guid>())).ReturnsAsync(_user);
			mockUserRepository.Setup(service => service.CreateUser(It.IsAny<User>())).ReturnsAsync(true);
			mockUserRepository.Setup(service => service.CreateUsers(It.IsAny<IEnumerable<User>>())).ReturnsAsync(true);
			mockUserRepository.Setup(service => service.UpdateUser(It.IsAny<long>(), It.IsAny<Domain.Models.User>())).ReturnsAsync(true);
			mockUserRepository.Setup(service => service.DeleteUser(It.IsAny<long>())).ReturnsAsync(true);
			_userService = new UserService(mockUserRepository.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetUsers_Returns_Users()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var expectedResult = _users;
			// Act
			var response = await _userService.GetUsers(tenantId);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetUser_Returns_User()
		{
			// Arrange
			var tenantId = Guid.NewGuid();
			var userId = 1;
			var expectedResult = _user;
			// Act
			var response = await _userService.GetUser(userId, tenantId);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateUser_Returns_True()
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
			var expectedResult = true;
			// Act
			var response = await _userService.CreateUser(user);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateUsers_Returns_True()
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
			var expectedResult = true;
			// Act
			var response = await _userService.CreateUsers(users);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateUser_Returns_True()
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
			var expectedResult = true;
			// Act
			var response = await _userService.UpdateUser(user.Id, user);
			// Assert
			Assert.Equal(response, expectedResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteUser_Returns_True()
		{
			// Arrange
			var userId = 1;
			var expectedResult = true;
			// Act
			var response = await _userService.DeleteUser(userId);
			// Assert
			Assert.Equal(response, expectedResult);
		}
	}
}
