using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class EventType : Base
{
    protected EventType() { }

    [SetsRequiredMembers]
    public EventType(string Name, string Description, Guid WorkId, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
        this.Description = Description;
        this.WorkId = WorkId;
    }

    public required Guid WorkId { get; init; }
public required string Name { get; init; }
public required string Description { get; init; }

public ICollection<ActivityEventType>? ActivitiesEventsTypes { get; init; }
public ICollection<Event>? Events { get; init; }

public Work? Work { get; init; }
}
