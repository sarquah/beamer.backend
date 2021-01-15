using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistance.Contexts;
using ProjectManagement.Infrastructure.Persistance.Repositories;
using ProjectManagement.Infrastructure.Services;

namespace ProjectManagement.API
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
            services.AddCors();
            var connection = @"Server=(localdb)\mssqllocaldb;Database=ProjectManagement;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
                // TODO Fix this
                // Remove reference loop when returning data
                //.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();
            logger.LogInformation("Running in development environment...");
            app.UseCors(builder => 
                builder.WithOrigins("http://localhost:3000", "http://localhost:9009", "http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseMvc();
        }
    }
}
