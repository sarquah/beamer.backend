using System.Collections.Generic;

namespace ProjectManagement.Domain.Models
{
    public class ProjectDTO : ActivityDTO
    {
        public virtual ICollection<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
    }
}
