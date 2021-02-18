using Beamer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Domain.Repositories
{
	public interface ITimeRegistrationRepository
	{
		Task<IEnumerable<TimeRegistration>> GetTimeRegistrationsForTask(long taskId, Guid tenantId);
		Task<TimeRegistration> GetTimeRegistration(long id, Guid tenantId);
		Task<bool> CreateTimeRegistration(TimeRegistration timeRegistration, Guid tenantId);
		Task<bool> UpdateTimeRegistration(long id, TimeRegistration timeRegistration, Guid tenantId);
		Task<bool> DeleteTimeRegistration(long id, Guid tenantId);
	}
}
