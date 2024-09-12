namespace Tsu.IndividualPlan.Domain.Dto.EventType;

public class EventTypeAssignDto(Guid EventTypeId, Guid ActivityId)
{
    public Guid EventTypeId { get; init; } = EventTypeId;
    public Guid ActivityId { get; init; } = ActivityId;
}