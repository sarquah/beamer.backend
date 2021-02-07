using Beamer.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Beamer.UnitTest.Repositories
{
	public abstract class Repository_UnitTest
	{
		protected AppDbContext _context;

		public Repository_UnitTest()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase("BeamerDB.UnitTest")
				.EnableSensitiveDataLogging()
				.Options;
			_context = new AppDbContext(options);
			_context.Database.EnsureCreated();
		}
	}
}
