using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPI.Models
{
    public class Task
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long TaskOwnerId { get; set; }
        [ForeignKey("TaskOwnerId")]
        public User TaskOwner { get; set; }
        public long ProjectId { get; set; }
    }
}
