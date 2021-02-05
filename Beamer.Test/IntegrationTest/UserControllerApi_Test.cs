using Newtonsoft.Json;
using Beamer.Domain.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Task = System.Threading.Tasks.Task;
using System.Threading.Tasks;
using System;

namespace Beamer.IntegrationTests
{
	public class UserControllerApi_Test : IntegrationTest
    {
        public UserControllerApi_Test() : base() { }

        [Fact]
        public async Task Get_Users_Test()
        {
            // Arrange
            var createdUser = await CreateTestUser();
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/user/users?tenantId={createdUser.TenantId}");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_User_Test()
        {
            // Arrange
            var createdUser = await CreateTestUser();
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/user/{createdUser.Id}?tenantId={createdUser.TenantId}");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_User_Test()
        {
            // Arrange
            var user = new StringContent(JsonConvert.SerializeObject(new User()
            {
                Name = "Test Integration User",
                Department = "Test Department",
                Role = "Tester",
                Email = "Test@Test.com",
                TenantId = Guid.NewGuid()
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync($"/api/v1/user", user);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Create_Users_Test()
        {
            // Arrange
            var user = new User()
            {
                Name = "Test Integration User",
                Department = "Test Department",
                Role = "Tester",
                Email = "Test@Test.com",
                TenantId = Guid.NewGuid()
            };
            var users = new List<User>();
            users.Add(user);
            var usersRequest = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync($"/api/v1/user/users", usersRequest);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Update_User_Test()
        {
            // Arrange
            var createdUser = await CreateTestUser();
            var user = new StringContent(JsonConvert.SerializeObject(new User()
            {
                Id = createdUser.Id,
                Name = "Test Integration User 2",
                Department = "Test Department",
                Role = "Tester",
                Email = "Test@Test.com",
                TenantId = createdUser.TenantId
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PutAsync($"/api/v1/user/{createdUser.Id}", user);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_User_Test()
        {
            // Arrange
            var createdUser = await CreateTestUser();
            // Act
            var response = await _client.DeleteAsync($"/api/v1/user/{createdUser.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
