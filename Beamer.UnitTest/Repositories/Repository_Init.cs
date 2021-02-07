using Beamer.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Beamer.UnitTest.Repositories
{
	public abstract class Repository_Init
	{
		protected AppDbContext _context;

		public Repository_Init()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase("BeamerDB.UnitTest")
				.EnableSensitiveDataLogging()
				.Options;
			_context = new AppDbContext(options);
			_context.Database.EnsureDeleted();
		}
	}
}
