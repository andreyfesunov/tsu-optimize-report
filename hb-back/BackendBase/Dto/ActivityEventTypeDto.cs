using BackendBase.Models;

namespace BackendBase.Dto
{
    public class ActivityEventTypeDto
    {
        public string Id { get; set; }
        public ActivityDto Activity { get; set; }
        public EventTypeDto EventType { get; set; }
    }
}
