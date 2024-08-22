namespace BackendBase.Dto;

public class ActivityEventTypeDto
{
    public ActivityEventTypeDto(Guid Id, ActivityDto? Activity, EventTypeDto? EventType)
    {
        this.Id = Id;
        this.Activity = Activity;
        this.EventType = EventType;
    }

    public readonly Guid Id;
    public readonly ActivityDto? Activity;
    public readonly EventTypeDto? EventType;
}
