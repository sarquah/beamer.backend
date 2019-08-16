using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using ProjectManagement.API;
using ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Xunit.Priority;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagement.IntegrationTests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class UserControllerApi_Test
    {
        private readonly HttpClient _client;

        public UserControllerApi_Test()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory, Priority(2)]
        [InlineData("GET")]
        public async Task GetUsersTest(string method)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/v1/user/users");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory, Priority(2), InlineData("GET", 6)]
        public async Task GetUserTest(string method, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/v1/user/{id}");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, Priority(0)]
        public async Task CreateUserTest()
        {
            // Arrange
            var user = new StringContent(JsonConvert.SerializeObject(new User()
            {
                Name = "Test Integration User",
                Department = "Test Department",
                Role = "Tester"
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync($"/api/v1/user", user);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory, Priority(1), InlineData(6)]
        public async Task UpdateUserTest(long id)
        {
            // Arrange
            var user = new StringContent(JsonConvert.SerializeObject(new User()
            {
                Id = id,
                Name = "Test Integration User 2",
                Department = "Test Department",
                Role = "Tester"
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PutAsync($"/api/v1/user/{id}", user);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task DeleteUserTest()
        {
            // Arrange
            var requestGet = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/user/users");
            var responseGet = await _client.SendAsync(requestGet);
            var usersJson = await responseGet.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            // Act
            var response = await _client.DeleteAsync($"/api/v1/user/{users.Last().Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
