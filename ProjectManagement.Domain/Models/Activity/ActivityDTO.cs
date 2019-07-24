namespace ProjectManagement.Domain.Models
{
    public class ActivityDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public EStatus Status { get; set; }
        public string OwnerName { get; set; }
    }
}
