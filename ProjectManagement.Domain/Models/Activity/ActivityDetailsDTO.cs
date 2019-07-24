using System;

namespace ProjectManagement.Domain.Models
{
    class ActivityDetailsDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OwnerName { get; set; }
    }
}
