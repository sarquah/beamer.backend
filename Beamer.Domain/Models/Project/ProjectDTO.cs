using System.Collections.Generic;

namespace Beamer.Domain.Models
{
    public class ProjectDTO : ActivityDTO
    {
        public ICollection<TaskDTO> Tasks { get; set; }

		public ProjectDTO()
		{
			Tasks = new List<TaskDTO>();
		}
    }
}
