using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class LessonType : Base
{
    protected LessonType()
    {
    }

    [SetsRequiredMembers]
    public LessonType(string Name, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
    }

    public required string Name { get; init; }

    public ICollection<Lesson>? Lessons { get; init; }
    public ICollection<Record>? Records { get; init; }
}