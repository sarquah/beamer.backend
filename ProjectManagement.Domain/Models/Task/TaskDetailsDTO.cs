namespace ProjectManagement.Domain.Models
{
    public class TaskDetailsDTO : ActivityDetailsDTO
    {
        public long? ProjectId { get; set; }
    }
}
