﻿using Beamer.API;
using Beamer.Domain.Models;
using Beamer.Infrastructure.Persistance.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMotions.Fake.Authentication.JwtBearer;

namespace Beamer.IntegrationTest
{
	public abstract class IntegrationTest
	{
		protected readonly HttpClient _client;

		public IntegrationTest()
		{
			var server = new TestServer(new WebHostBuilder()
				.UseEnvironment("Testing")
				.UseStartup<Startup>()
				.ConfigureServices(services =>
				{
					var descriptor = services.SingleOrDefault(
						d => d.ServiceType == typeof(DbContextOptions<AppDbContext>)
					);
					services.Remove(descriptor);
					services.AddDbContext<AppDbContext>(options =>
					{
						options.UseInMemoryDatabase("BeamerDB.IntegrationTest");
					});
					services.AddLogging(builder => builder.AddConsole());
				})
				.UseTestServer()
				.ConfigureTestServices(collection =>
				{
					collection.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme).AddFakeJwtBearer();
				}));
			_client = server.CreateClient();

			dynamic data = new ExpandoObject();
			data.sub = Guid.NewGuid();
			data.role = new[] { "sub_role", "admin" };

			_client.SetFakeBearerToken((object)data);
		}

		protected async Task<Project> CreateTestProject()
		{
			var user = await CreateTestUser();
			var project = new Project()
			{
				Name = "Test Integration Project",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is an integration test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid(),
				OwnerId = user.Id
			};
			var projectRequest = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");
			var response = await _client.PostAsync($"/api/v1/project", projectRequest);
			response.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<Project>(await response.Content.ReadAsStringAsync());
		}

		protected async Task<Domain.Models.Task> CreateTestTask()
		{
			var user = await CreateTestUser();
			var project = await CreateTestProject();
			var task = new Domain.Models.Task()
			{
				Name = "Test Integration Task",
				StartDate = new DateTime(2019, 8, 8),
				EndDate = new DateTime(2019, 8, 12),
				Description = "This is an integration test",
				Status = EStatus.NotStarted,
				TenantId = Guid.NewGuid(),
				OwnerId = user.Id,
				ProjectId = project.Id
			};
			var taskRequest = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
			var response = await _client.PostAsync($"/api/v1/task", taskRequest);
			response.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<Domain.Models.Task>(await response.Content.ReadAsStringAsync());
		}

		protected async Task<User> CreateTestUser()
		{
			var user = new User()
			{
				Name = "Test Integration User",
				Department = "Development",
				Email = "Test@Test.com",
				Role = "Tester",
				TenantId = Guid.NewGuid()
			};
			var userRequest = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
			var response = await _client.PostAsync($"/api/v1/user", userRequest);
			response.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
		}

		protected async Task<TimeRegistration> CreateTestTimeRegistration()
		{
			var task = await CreateTestTask();
			var user = await CreateTestUser();
			var timeRegistration = new TimeRegistration()
			{
				StartDate = new DateTime(2021, 2, 19, 9, 0, 0),
				EndDate = new DateTime(2021, 2, 19, 12, 0, 0),
				TenantId = Guid.NewGuid(),
				TaskId = task.Id,
				OwnerId = user.Id
			};
			var timeRegistrationRequest = new StringContent(JsonConvert.SerializeObject(timeRegistration), Encoding.UTF8, "application/json");
			var response = await _client.PostAsync($"/api/v1/timeregistration?tenantId={timeRegistration.TenantId}", timeRegistrationRequest);
			response.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<TimeRegistration>(await response.Content.ReadAsStringAsync());
		}
	}
}
