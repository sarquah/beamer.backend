using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Models
{
    public class Activity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long? OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
