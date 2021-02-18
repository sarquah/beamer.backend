using AutoMapper;
using Beamer.Domain.Models;
using Beamer.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamer.API.Controllers
{
	[Route("api/v1/timeregistration")]
	[ApiController]
	[Authorize]
	public class TimeRegistrationController : ControllerBase
	{
		private readonly ITimeRegistrationService _timeRegistrationService;
		private readonly IMapper _mapper;

		public TimeRegistrationController(ITimeRegistrationService timeRegistrationService, IMapper mapper)
		{
			_timeRegistrationService = timeRegistrationService;
			_mapper = mapper;
		}

		// GET: api/v1/timeregistration/timeregistrations
		[HttpGet("timeregistrations")]
		public async Task<ActionResult<IEnumerable<TimeRegistration>>> GetTimeRegistrationsForTask(long taskId, Guid tenantId)
		{
			var timeRegistrations = await _timeRegistrationService.GetTimeRegistrationsForTask(taskId, tenantId);
			return Ok(timeRegistrations);
		}

		// GET: api/v1/timeregistration/1
		[HttpGet("{id}")]
		public async Task<ActionResult<TimeRegistration>> GetTimeRegistration(long id, Guid tenantId)
		{
			var timeRegistration = await _timeRegistrationService.GetTimeRegistration(id, tenantId);
			if (timeRegistration == null)
			{
				return NotFound();
			}
			return timeRegistration;
		}

		// POST: api/v1/timeregistration
		[HttpPost]
		public async Task<ActionResult> CreateTimeRegistration(TimeRegistration timeRegistration, Guid tenantId)
		{
			bool success = await _timeRegistrationService.CreateTimeRegistration(timeRegistration, tenantId);
			return CreatedAtAction(nameof(GetTimeRegistration), new { id = timeRegistration.Id }, timeRegistration);
		}

		// PUT: api/v1/timeregistration/1
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateTimeRegistration(long id, TimeRegistration timeRegistration, Guid tenantId)
		{
			if (id != timeRegistration.Id)
			{
				return NotFound();
			}
			bool success = await _timeRegistrationService.UpdateTimeRegistration(id, timeRegistration, tenantId);
			if (!success)
			{
				return NotFound();
			}
			return NoContent();
		}

		// DELETE: api/v1/timeregistration/1
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteTimeRegistration(long id, Guid tenantId)
		{
			bool success = await _timeRegistrationService.DeleteTimeRegistration(id, tenantId);
			if (!success)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
