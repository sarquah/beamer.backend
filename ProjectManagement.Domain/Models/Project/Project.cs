using System.Collections.Generic;

namespace ProjectManagement.Domain.Models
{
    public class Project : Activity
    {
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
