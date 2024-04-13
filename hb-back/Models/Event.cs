namespace BackendBase.Models;

public class Event : Base
{
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<EventFile> EventsFiles { get; set; }
    public EventType EventType { get; set; }
    public StateUser StateUser { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
}