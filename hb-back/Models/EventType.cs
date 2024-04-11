using System.Collections;

namespace BackendBase.Models;

public class EventType : Base
{
    public ICollection<Activity> Activities { get; set; }
    
    public string Name { get; set; }
}