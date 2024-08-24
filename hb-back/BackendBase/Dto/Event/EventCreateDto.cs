namespace BackendBase.Dto.Event;

public class EventCreateDto
{
    public EventCreateDto(Guid StateUserId, Guid EventTypeId, DateTime StartedAt, DateTime EndedAt)
    {
        this.StateUserId = StateUserId;
        this.EventTypeId = EventTypeId;
        this.StartedAt = StartedAt;
        this.EndedAt = EndedAt;
    }

    public Guid EventTypeId { get; init; }
    public Guid StateUserId { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime EndedAt { get; init; }
}
