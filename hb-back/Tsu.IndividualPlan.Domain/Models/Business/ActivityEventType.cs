using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class ActivityEventType : Base
{
    protected ActivityEventType()
    {
    }

    [SetsRequiredMembers]
    public ActivityEventType(Guid ActivityId, Guid EventTypeId, Guid? Id = null)
        : base(Id)
    {
        this.ActivityId = ActivityId;
        this.EventTypeId = EventTypeId;
    }

    public required Guid ActivityId { get; init; }
    public required Guid EventTypeId { get; init; }
    public Activity? Activity { get; init; }
    public EventType? EventType { get; init; }
}