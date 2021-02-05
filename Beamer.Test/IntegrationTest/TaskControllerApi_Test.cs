using Newtonsoft.Json;
using Beamer.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Task = System.Threading.Tasks.Task;
using System.Threading.Tasks;

namespace Beamer.IntegrationTests
{
	public class TaskControllerApi_Test : IntegrationTest
    {
        public TaskControllerApi_Test() : base() { }

        [Fact]
        public async Task Get_Tasks_Test()
        {
            // Arrange
            var createdTask = await CreateTestTask();
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/task/tasks?tenantId={createdTask.TenantId}");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Task_Test()
        {
            // Arrange
            var createdTask = await CreateTestTask();
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/task/{createdTask.Id}?tenantId={createdTask.TenantId}");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_Task_Test()
        {
            // Arrange
            var task = new StringContent(JsonConvert.SerializeObject(new Domain.Models.Task()
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

        [Fact]
        public async Task Update_Task_Test()
        {
            // Arrange
            var createdTask = await CreateTestTask();
            var task = new StringContent(JsonConvert.SerializeObject(new Domain.Models.Task()
            {
                Id = createdTask.Id,
                Name = "Test Integration Task 2",
                StartDate = new DateTime(2019, 8, 8),
                EndDate = new DateTime(2019, 8, 20),
                Description = "This is an integration task test update",
                Status = EStatus.NotStarted,
                TenantId = createdTask.TenantId
            }), Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PutAsync($"/api/v1/task/{createdTask.Id}", task);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Task_Test()
        {
            // Arrange
            var createdTask = await CreateTestTask();           
            // Act
            var response = await _client.DeleteAsync($"/api/v1/task/{createdTask.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task<Domain.Models.Task> CreateTestTask()
        {
            var task = new Domain.Models.Task()
            {
                Name = "Test Integration Task",
                StartDate = new DateTime(2019, 8, 8),
                EndDate = new DateTime(2019, 8, 12),
                Description = "This is an integration test",
                Status = EStatus.NotStarted,
                TenantId = Guid.NewGuid()
            };
            var taskRequest = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/v1/task", taskRequest);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject< Domain.Models.Task> (await response.Content.ReadAsStringAsync());
        }
    }
}
