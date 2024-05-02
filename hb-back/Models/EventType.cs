using System.Collections;

namespace BackendBase.Models;

public class EventType : Base
{
    public ICollection<ActivityEventType> ActivitiesEventsTypes { get; set; }
    public ICollection<Event> Events { get; set; }
    public Guid WorkId { get; set; }
    public Work Work { get; set; }
    public string Name { get; set; }
}