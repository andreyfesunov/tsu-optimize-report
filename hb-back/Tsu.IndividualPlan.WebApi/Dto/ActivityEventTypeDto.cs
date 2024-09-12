namespace Tsu.IndividualPlan.WebApi.Dto;

public class ActivityEventTypeDto(Guid Id, ActivityDto? Activity, EventTypeDto? EventType)
{
    public Guid Id { get; init; } = Id;
    public ActivityDto? Activity { get; init; } = Activity;
    public EventTypeDto? EventType { get; init; } = EventType;
}