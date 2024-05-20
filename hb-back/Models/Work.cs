namespace BackendBase.Models;

public class Work : Base
{
    public List<EventType> EventsTypes { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
}