using System;

namespace Beamer.Domain.Models
{
	public class TimeRegistration
	{
		public long Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public long? OwnerId { get; set; }
		public User Owner { get; set; }
		public long? TaskId { get; set; }
		public Task Task { get; set; }
		public Guid TenantId { get; set; }

		public double GetHoursSpent() => EndDate.Subtract(StartDate).TotalHours;
	}
}
