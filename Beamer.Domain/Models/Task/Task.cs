using System.Collections.Generic;
using System.Linq;

namespace Beamer.Domain.Models
{
    public class Task : Activity
    {
        public long? ProjectId { get; set; }
        public Project Project { get; set; }
		public ICollection<TimeRegistration> TimeRegistrations { get; set; }

		public Task()
		{
			TimeRegistrations = new List<TimeRegistration>();
		}

		public double GetHoursSpent() => TimeRegistrations.Sum(t => t.GetHoursSpent());
	}
}
