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

    public readonly Guid EventTypeId;
    public readonly Guid StateUserId;
    public readonly DateTime StartedAt;
    public readonly DateTime EndedAt;
}
