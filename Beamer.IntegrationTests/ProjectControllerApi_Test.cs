using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Beamer.API;
using Beamer.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Xunit.Priority;
using Task = System.Threading.Tasks.Task;

namespace Beamer.IntegrationTests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ProjectControllerApi_Test
    {
        private readonly HttpClient _client;

        public ProjectControllerApi_Test()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory, Priority(2)]
        [InlineData("GET")]
        public async Task GetProjectsTest(string method)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/v1/project/projects");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory, Priority(2), InlineData("GET", 1)]
        public async Task GetProjectTest(string method, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/v1/project/{id}");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, Priority(0)]
        public async Task CreateProjectTest()
        {
            // Arrange
            var project = new StringContent(JsonConvert.SerializeObject(new Project()
            {
                Name = "Test Integration Project",
                StartDate = new DateTime(2019, 8, 8),
                EndDate = new DateTime(2019, 8, 12),
                Description = "This is an integration test",
                Status = EStatus.NotStarted
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync($"/api/v1/project", project);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory, Priority(1), InlineData(1)]
        public async Task UpdateProjectTest(long id)
        {
            // Arrange
            var project = new StringContent(JsonConvert.SerializeObject(new Project()
            {
                Id = id,
                Name = "Test Integration Project 2",
                StartDate = new DateTime(2019, 8, 8),
                EndDate = new DateTime(2019, 8, 20),
                Description = "This is an integration test update",
                Status = EStatus.NotStarted
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PutAsync($"/api/v1/project/{id}", project);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task DeleteProjectTest()
        {
            // Arrange
            var requestGet = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/project/projects");
            var responseGet = await _client.SendAsync(requestGet);
            var projectsJson = await responseGet.Content.ReadAsStringAsync();
            var projects = JsonConvert.DeserializeObject<List<Project>>(projectsJson);
            // Act
            var response = await _client.DeleteAsync($"/api/v1/project/{projects.Last().Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
