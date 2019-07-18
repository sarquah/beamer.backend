using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPI.Models
{
    public class Task : Activity
    {
        public long? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
