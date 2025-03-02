using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class Record : Base
{
    protected Record()
    {
    }

    [SetsRequiredMembers]
    public Record(Guid StateUserId, Guid ActivityId, Guid LessonTypeId, int Hours, string GroupString, Guid? Id = null)
        : base(Id)
    {
        this.StateUserId = StateUserId;
        this.ActivityId = ActivityId;
        this.LessonTypeId = LessonTypeId;
        this.Hours = Hours;
        this.GroupString = GroupString;
    }

    public required Guid StateUserId { get; init; }
    public required Guid ActivityId { get; init; }

    public required Guid LessonTypeId { get; init; }

    public required int Hours { get; init; }
    public string GroupString { get; init; }

    public Activity? Activity { get; init; }
    public StateUser? StateUser { get; init; }
    public LessonType? LessonType { get; init; }
}