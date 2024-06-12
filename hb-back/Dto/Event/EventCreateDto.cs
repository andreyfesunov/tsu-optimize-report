namespace BackendBase.Dto.Event;

public class EventCreateDto
{
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public string EventTypeId { get; set; }
    public string StateUserId { get; set; }
}