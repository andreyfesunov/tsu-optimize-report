namespace BackendBase.Models;

public class Work : Base
{
    public ICollection<EventType> EventsTypes { get; set; }
    public string Name { get; set; }
}