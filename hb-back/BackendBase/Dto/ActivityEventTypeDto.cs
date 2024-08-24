namespace BackendBase.Dto;

public class ActivityEventTypeDto
{
    public ActivityEventTypeDto(Guid Id, ActivityDto? Activity, EventTypeDto? EventType)
    {
        this.Id = Id;
        this.Activity = Activity;
        this.EventType = EventType;
    }

    public Guid Id { get; init; }
    public ActivityDto? Activity { get; init; }
    public EventTypeDto? EventType { get; init; }
}
