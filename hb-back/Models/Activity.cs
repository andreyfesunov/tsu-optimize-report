namespace BackendBase.Models;

public class Activity : Base
{
    public ICollection<EventType> EventTypes { get; set; }
    
    public string Name { get; set; }
}