using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class Activity : Base
{
    protected Activity() { }

    [SetsRequiredMembers]
    public Activity(string Name, int Column, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
        this.Column = Column;
    }

    public required string Name { get; init; }
    public required int Column { get; init; }

    public ICollection<ActivityEventType>? ActivitiesEventsTypes { get; init; }
    public ICollection<Record>? Records { get; init; }
}
