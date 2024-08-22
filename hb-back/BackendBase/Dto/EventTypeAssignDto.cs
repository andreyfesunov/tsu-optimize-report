namespace BackendBase.Dto;

public class EventTypeAssignDto
{
    public EventTypeAssignDto(Guid EventTypeId, Guid ActivityId)
    {
        this.EventTypeId = EventTypeId;
        this.ActivityId = ActivityId;
    }

    public readonly Guid EventTypeId;
    public readonly Guid ActivityId;
}
