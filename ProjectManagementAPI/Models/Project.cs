using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPI.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long ProjectOwnerId { get; set; }
        [ForeignKey("ProjectOwnerId")]
        public User ProjectOwner { get; set; }
        public List<Task> Tasks { get; set; }

        public Project()
        {
            Tasks = new List<Task>();
        }
    }
}
