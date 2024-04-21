namespace BackendBase.Models;

public class Activity : Base
{
    public ICollection<ActivityEventType> ActivitiesEventsTypes { get; set; }
    public ICollection<Record> Records { get; set; }
    public string Name { get; set; }
    public string Column { get; set; }
}