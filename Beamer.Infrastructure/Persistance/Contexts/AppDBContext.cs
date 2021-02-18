using Microsoft.EntityFrameworkCore;
using Beamer.Domain.Models;

namespace Beamer.Infrastructure.Persistance.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
		public DbSet<TimeRegistration> TimeRegistrations { get; set; }
	}
}
