using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.WebApi.Models;

public class Lesson : Base
{
    protected Lesson()
    {
    }

    [SetsRequiredMembers]
    public Lesson(
        Guid EventId,
        Guid LessonTypeId,
        int? PlanDate = null,
        int? FactDate = null,
        Guid? Id = null
    )
        : base(Id)
    {
        this.EventId = EventId;
        this.LessonTypeId = LessonTypeId;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public required Guid EventId { get; init; }
public required Guid LessonTypeId { get; init; }

/**
 * Editable Fields
 */
public int? FactDate { get; set; }

public int? PlanDate { get; set; }

public Event? Event { get; init; }
public LessonType? LessonType { get; init; }
}