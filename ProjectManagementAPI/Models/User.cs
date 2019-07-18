using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        [InverseProperty("ProjectOwner")]
        public List<Project> Projects { get; set; } = new List<Project>();
        [InverseProperty("TaskOwner")]
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
