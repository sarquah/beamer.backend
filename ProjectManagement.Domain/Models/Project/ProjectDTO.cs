using System.Collections.Generic;

namespace ProjectManagement.Domain.Models
{
    class ProjectDTO : ActivityDTO
    {
        public virtual ICollection<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
    }
}
