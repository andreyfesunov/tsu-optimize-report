namespace Tsu.IndividualPlan.WebApi.Dto;

public class EventTypeAssignDto
{
    public EventTypeAssignDto(Guid EventTypeId, Guid ActivityId)
    {
        this.EventTypeId = EventTypeId;
        this.ActivityId = ActivityId;
    }

    public Guid EventTypeId { get; init; }
    public Guid ActivityId { get; init; }
}