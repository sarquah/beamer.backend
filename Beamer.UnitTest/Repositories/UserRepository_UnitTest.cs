using Beamer.Domain.Models;
using Beamer.Infrastructure.Persistance.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Beamer.UnitTest.Repositories
{
	public class UserRepository_UnitTest : Repository_UnitTest
	{
		private UserRepository sut;
		private User user;
		private IEnumerable<User> users;

		public UserRepository_UnitTest() : base()
		{

			sut = new UserRepository(_context);
			user = new User()
			{
				Name = "Test Person",
				Department = "Development",
				Email = "test@test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			users = new List<User>()
			{
				user
			};
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_Users_Returns_Users()
		{
			// Arrange
			await sut.CreateUser(user);
			var expectedResult = JsonConvert.SerializeObject(users);
			// Act
			var result = await sut.GetUsers(user.TenantId);
			var stringResult = JsonConvert.SerializeObject(result);
			// Assert
			Assert.Equal(expectedResult, stringResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_User_Returns_User()
		{
			// Arrange
			await sut.CreateUser(user);
			var getUsers = await sut.GetUsers(user.TenantId);
			user.Id = getUsers.First().Id;
			var expectedResult = JsonConvert.SerializeObject(user);
			// Act
			var result = await sut.GetUser(user.Id, user.TenantId);
			var stringResult = JsonConvert.SerializeObject(result);
			// Assert
			Assert.Equal(expectedResult, stringResult);
		}

		[Fact]
		public async System.Threading.Tasks.Task Create_User_Returns_True()
		{
			// Arrange
			var expectedResult = true;
			// Act
			var result = await sut.CreateUser(user);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public async System.Threading.Tasks.Task Create_Users_Returns_True()
		{
			// Arrange
			var expectedResult = true;
			// Act
			var result = await sut.CreateUsers(users);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public async System.Threading.Tasks.Task Update_User_Returns_True()
		{
			// Arrange
			await sut.CreateUser(user);
			_context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getUsers = await sut.GetUsers(user.TenantId);
			var id = getUsers.First().Id;
			var expectedResult = true;
			var updatedUser = new User()
			{
				Id = id,
				Name = "Test Person Update",
				Department = "Test Department",
				Email = "test@test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			// Act
			var result = await sut.UpdateUser(id, updatedUser);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public async System.Threading.Tasks.Task Delete_User_Returns_True()
		{
			// Arrange		
			await sut.CreateUser(user);
			_context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
			var getUsers = await sut.GetUsers(user.TenantId);
			var id = getUsers.First().Id;
			var expectedResult = true;
			// Act
			var result = await sut.DeleteUser(id);
			// Assert
			Assert.Equal(expectedResult, result);
		}
	}
}
