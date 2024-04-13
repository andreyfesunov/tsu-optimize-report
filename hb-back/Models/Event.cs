namespace BackendBase.Models;

public class Event : Base
{
    public ICollection<Lesson> Lessons { get; set; }

    public Work Work { get; set; }
    public EventType EventType { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
}