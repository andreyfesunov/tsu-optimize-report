namespace BackendBase.Models
{
    public class EventFile : Base
    {
        public Guid FileId { get; set; }
        public File File { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
