using Beamer.Domain.Models;
using Beamer.Domain.Repositories;
using Beamer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.Infrastructure.Services
{
	public class TimeRegistrationService : ITimeRegistrationService
	{
		private readonly ITimeRegistrationRepository _timeRegistrationRepository;

		public TimeRegistrationService(ITimeRegistrationRepository timeRegistrationRepository)
		{
			_timeRegistrationRepository = timeRegistrationRepository;
		}

		public async Task<IEnumerable<TimeRegistration>> GetTimeRegistrationsForTask(long taskId, Guid tenantId) => await _timeRegistrationRepository.GetTimeRegistrationsForTask(taskId, tenantId);
		public async Task<TimeRegistration> GetTimeRegistration(long id, Guid tenantId) => await _timeRegistrationRepository.GetTimeRegistration(id, tenantId);
		public async Task<bool> CreateTimeRegistration(TimeRegistration timeRegistration, Guid tenantId) => await _timeRegistrationRepository.CreateTimeRegistration(timeRegistration, tenantId);
		public async Task<bool> UpdateTimeRegistration(long id, TimeRegistration timeRegistration, Guid tenantId) => await _timeRegistrationRepository.UpdateTimeRegistration(id, timeRegistration, tenantId);
		public async Task<bool> DeleteTimeRegistration(long id, Guid tenantId) => await _timeRegistrationRepository.DeleteTimeRegistration(id, tenantId);
	}
}
