using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beamer.Infrastructure.Persistance.Repositories
{
	public class TimeRegistrationRepository : BaseRepository, ITimeRegistrationRepository
	{
		public TimeRegistrationRepository(AppDbContext context) : base(context) { }

		public async Task<bool> CreateTimeRegistration(TimeRegistration timeRegistration, Guid tenantId)
		{
			try
			{
				_context.TimeRegistrations.Add(timeRegistration);
				await _context.SaveChangesAsync();
				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		public async Task<bool> DeleteTimeRegistration(long id, Guid tenantId)
		{
			var timeRegistration = await _context.TimeRegistrations.FindAsync(id);
			if (timeRegistration == null)
			{
				return false;
			}
			try
			{
				_context.TimeRegistrations.Remove(timeRegistration);
				await _context.SaveChangesAsync();
				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		public async Task<TimeRegistration> GetTimeRegistration(long id, Guid tenantId)
		{
			return await _context.TimeRegistrations.AsNoTracking().Include(t => t.Owner).FirstOrDefaultAsync(t => t.Id == id && t.TenantId == tenantId);
		}

		public async Task<IEnumerable<TimeRegistration>> GetTimeRegistrationsForTask(long taskId, Guid tenantId)
		{
			return await _context.TimeRegistrations
				.AsNoTracking()
				.Where(t => t.TaskId == taskId && t.TenantId == tenantId)
				.Include(t => t.Owner)
				.ToListAsync();
		}

		public async Task<bool> UpdateTimeRegistration(long id, TimeRegistration timeRegistration, Guid tenantId)
		{
			var foundTimeRegistration = await _context.TimeRegistrations.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
			if (foundTimeRegistration == null)
			{
				return false;
			}
			try
			{
				_context.Entry(timeRegistration).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}
	}
}
