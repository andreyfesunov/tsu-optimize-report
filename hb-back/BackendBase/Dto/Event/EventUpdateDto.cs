namespace BackendBase.Dto.Event;

public class EventUpdateDto
{
    public EventUpdateDto(Guid Id, DateTime StartedAt, DateTime EndedAt)
    {
        this.Id = Id;
        this.StartedAt = StartedAt;
        this.EndedAt = EndedAt;
    }

    public readonly Guid Id;
    public readonly DateTime StartedAt;
    public readonly DateTime EndedAt;
}
