namespace BackendBase.Models
{
    public class ActivityEventType : Base
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; }
    }
}
