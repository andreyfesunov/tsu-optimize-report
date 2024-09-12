using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class Activity : Base
{
    protected Activity()
    {
    }

    [SetsRequiredMembers]
    public Activity(string Name, int Column, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
        this.Column = Column;
    }

    public required string Name { get; set; }
    public required int Column { get; set; }

    public ICollection<ActivityEventType>? ActivitiesEventsTypes { get; set; }
    public ICollection<Record>? Records { get; set; }
}