using Beamer.API;
using Beamer.Infrastructure.Persistance.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebMotions.Fake.Authentication.JwtBearer;

namespace Beamer.IntegrationTests
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
						options.UseInMemoryDatabase("BeamerDB.Test");
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
	}
}
