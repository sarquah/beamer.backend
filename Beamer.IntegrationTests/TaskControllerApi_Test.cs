using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Beamer.API;
using Beamer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Xunit.Priority;
using Task = System.Threading.Tasks.Task;

namespace Beamer.IntegrationTests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class TaskControllerApi_Test
    {
        private readonly HttpClient _client;

        public TaskControllerApi_Test()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory, Priority(2)]
        [InlineData("GET")]
        public async Task GetTasksTest(string method)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/v1/task/tasks");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory, Priority(2), InlineData("GET", 6)]
        public async Task GetTaskTest(string method, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/v1/task/{id}");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, Priority(0)]
        public async Task CreateTaskTest()
        {
            // Arrange
            var task = new StringContent(JsonConvert.SerializeObject(new Beamer.Domain.Models.Task()
            {
                Name = "Test Integration Task",
                StartDate = new DateTime(2019, 8, 8),
                EndDate = new DateTime(2019, 8, 12),
                Description = "This is an integration task test",
                Status = EStatus.NotStarted
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync($"/api/v1/task", task);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory, Priority(1), InlineData(6)]
        public async Task UpdateTaskTest(long id)
        {
            // Arrange
            var task = new StringContent(JsonConvert.SerializeObject(new Beamer.Domain.Models.Task()
            {
                Id = id,
                Name = "Test Integration Task 2",
                StartDate = new DateTime(2019, 8, 8),
                EndDate = new DateTime(2019, 8, 20),
                Description = "This is an integration task test update",
                Status = EStatus.NotStarted
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PutAsync($"/api/v1/task/{id}", task);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task DeleteTaskTest()
        {
            // Arrange
            var requestGet = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/task/tasks");
            var responseGet = await _client.SendAsync(requestGet);
            var tasksJson = await responseGet.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<Beamer.Domain.Models.Task>>(tasksJson);
            // Act
            var response = await _client.DeleteAsync($"/api/v1/task/{tasks.Last().Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
