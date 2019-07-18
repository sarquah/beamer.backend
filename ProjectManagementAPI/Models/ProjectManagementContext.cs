using Microsoft.EntityFrameworkCore;

namespace ProjectManagementAPI.Models
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Project>()
        //        .HasOne(p => p.ProjectOwner)
        //        .WithMany(u => u.Projects)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<Task>()
        //        .HasOne(t => t.TaskOwner)
        //        .WithMany(u => u.Tasks)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
