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
        public List<Task> Tasks { get; set; }
        public List<Project> Projects { get; set; }

        public User()
        {
            Tasks = new List<Task>();
            Projects = new List<Project>();
        }
    }
}
