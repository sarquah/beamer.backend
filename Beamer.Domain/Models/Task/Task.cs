namespace Beamer.Domain.Models
{
    public class Task : Activity
    {
        public long? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
