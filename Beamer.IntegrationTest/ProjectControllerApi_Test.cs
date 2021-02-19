using Newtonsoft.Json;
using Beamer.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace Beamer.IntegrationTest
{
	public class ProjectControllerApi_Test : IntegrationTest
	{
		public ProjectControllerApi_Test() : base() { }		

		[Fact]
		public async Task Get_Projects_Test()	
		{
			// Arrange
			var createdProject = await CreateTestProject();
			var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/project/projects?tenantId={createdProject.TenantId}");
			// Act
			var response = await _client.SendAsync(request);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Get_Project_Test()
		{
			// Arrange
			var createdProject = await CreateTestProject();
			var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/project/{createdProject.Id}?tenantId={createdProject.TenantId}");
			// Act
			var response = await _client.SendAsync(request);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Create_Project_Test()
		{
			// Arrange
			var project = new StringContent(JsonConvert.SerializeObject(new Project()
			{
				Name = "Test Integration Project",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is an integration test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid()
			}
			), Encoding.UTF8, "application/json");
			var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/project");
			request.Content = project;
			// Act
			var response = await _client.SendAsync(request);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		[Fact]
		public async Task Update_Project_Test()
		{
			// Arrange		
			var createdProject = await CreateTestProject();

			var projectRequest = new StringContent(JsonConvert.SerializeObject(new Project()
			{
				Id = createdProject.Id,
				Name = "Test Integration Project 2",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 20),
				Description = "This is an integration test update",
				Status = EStatus.NotStarted,
				TenantId = createdProject.TenantId
			}), Encoding.UTF8, "application/json");
			// Act
			var response = await _client.PutAsync($"/api/v1/project/{createdProject.Id}", projectRequest);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}

		[Fact]
		public async Task Delete_Project_Test()
		{
			// Arrange			
			var createdProject = await CreateTestProject();
			// Act
			var response = await _client.DeleteAsync($"/api/v1/project/{createdProject.Id}");
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}
	}
}
