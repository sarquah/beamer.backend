using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Beamer.Domain.Repositories;
using Beamer.Domain.Services;
using Beamer.Infrastructure.Persistance.Contexts;
using Beamer.Infrastructure.Persistance.Repositories;
using Beamer.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace Beamer.API
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		private readonly ILoggerFactory _loggerFactory;

		public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
		{
			_configuration = configuration;
			_loggerFactory = loggerFactory;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(_configuration, "AzureActiveDirectory");
			services.AddCors();
			var connection = @"Server=(localdb)\mssqllocaldb;Database=Beamer;Trusted_Connection=True;ConnectRetryCount=0";
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
			services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Beamer API", Version = "v1" }));
			services.AddAutoMapper(typeof(Startup));
			//Add dependencies
			services.AddScoped<IProjectRepository, ProjectRepository>();
			services.AddScoped<IProjectService, ProjectService>();
			services.AddScoped<ITaskRepository, TaskRepository>();
			services.AddScoped<ITaskService, TaskService>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserService, UserService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			var logger = _loggerFactory.CreateLogger<Startup>();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beamer API V1");
				c.RoutePrefix = string.Empty;
			});
			app.UseDeveloperExceptionPage();
			logger.LogInformation("Running in development environment...");
			app.UseCors(builder =>
			{
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMvc();
		}
	}
}
