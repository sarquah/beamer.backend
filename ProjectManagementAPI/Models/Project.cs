using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPI.Models
{
    public class Project : Activity
    {
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
