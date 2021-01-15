using System.Collections.Generic;

namespace Beamer.Domain.Models
{
    public class ProjectDetailsDTO : ActivityDetailsDTO
    {
        public virtual ICollection<TaskDetailsDTO> Tasks { get; set; } = new List<TaskDetailsDTO>();
    }
}
