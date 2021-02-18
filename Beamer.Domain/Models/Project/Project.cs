using System.Collections.Generic;

namespace Beamer.Domain.Models
{
    public class Project : Activity
    {
        public ICollection<Task> Tasks { get; set; }

		public Project()
		{
            Tasks = new List<Task>();
		}

    }
}
