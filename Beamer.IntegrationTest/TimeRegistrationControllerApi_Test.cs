using Beamer.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Beamer.IntegrationTest
{
	public class TimeRegistrationControllerApi_Test : IntegrationTest
	{
		public TimeRegistrationControllerApi_Test() : base() { }

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistrationsForTask_Test()
		{
			// Arrange
			var createdTimeRegistration = await CreateTestTimeRegistration();
			var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/timeregistration/timeregistrations?taskId={createdTimeRegistration.TaskId}&tenantId={createdTimeRegistration.TenantId}");
			// Act
			var response = await _client.SendAsync(request);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetTimeRegistration_Test()
		{
			// Arrange
			var createdTimeRegistration = await CreateTestTimeRegistration();
			var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/timeregistration/{createdTimeRegistration.Id}?tenantId={createdTimeRegistration.TenantId}");
			// Act
			var response = await _client.SendAsync(request);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async System.Threading.Tasks.Task CreateTimeRegistration_Test()
		{
			// Arrange
			var timeRegistration = new TimeRegistration()
			{
				StartDate = new DateTime(2021, 2, 19, 9, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 12, 0, 0),
				TenantId = Guid.NewGuid()
			};
			var timeRegistrationRequest = new StringContent(JsonConvert.SerializeObject(timeRegistration), Encoding.UTF8, "application/json");
			var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/timeregistration?tenantId={timeRegistration.TenantId}");
			request.Content = timeRegistrationRequest;
			// Act
			var response = await _client.SendAsync(request);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateTimeRegistration_Test()
		{
			// Arrange		
			var createdTimeRegistration = await CreateTestTimeRegistration();

			var timeRegistrationRequest = new StringContent(JsonConvert.SerializeObject(new TimeRegistration()
			{
				Id = createdTimeRegistration.Id,
				StartDate = new DateTime(2021, 2, 19, 9, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 18, 0, 0),
				TenantId = createdTimeRegistration.TenantId,
				TaskId = createdTimeRegistration.TaskId,
				OwnerId = createdTimeRegistration.OwnerId
			}), Encoding.UTF8, "application/json");
			// Act
			var response = await _client.PutAsync($"/api/v1/timeregistration/{createdTimeRegistration.Id}?tenantId={createdTimeRegistration.TenantId}", timeRegistrationRequest);
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteTimeRegistration_Test()
		{
			// Arrange			
			var createdTimeRegistration = await CreateTestTimeRegistration();
			// Act
			var response = await _client.DeleteAsync($"/api/v1/timeregistration/{createdTimeRegistration.Id}?tenantId={createdTimeRegistration.TenantId}");
			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}
	}
}
