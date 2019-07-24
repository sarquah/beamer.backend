using System.Collections.Generic;

namespace ProjectManagement.Domain.Models
{
    public class ProjectDetailsDTO : ActivityDetailsDTO
    {
        public virtual ICollection<TaskDetailsDTO> Tasks { get; set; } = new List<TaskDetailsDTO>();
    }
}
