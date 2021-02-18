using System.Collections.Generic;

namespace Beamer.Domain.Models
{
    public class ProjectDetailsDTO : ActivityDetailsDTO
    {
        public ICollection<TaskDetailsDTO> Tasks { get; set; }

		public ProjectDetailsDTO()
		{
            Tasks = new List<TaskDetailsDTO>();
		}
    }
}
