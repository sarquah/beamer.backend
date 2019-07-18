using Microsoft.EntityFrameworkCore;

namespace ProjectManagementAPI.Models
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options){}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
