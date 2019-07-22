using System.Collections.Generic;

namespace ProjectManagementAPI.Models
{
    public class Project : Activity
    {
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
