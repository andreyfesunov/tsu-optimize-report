using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class LessonType : Base
{
    protected LessonType() { }

    [SetsRequiredMembers]
    public LessonType(string Name, Guid? Id = null)
        : base(Id) => this.Name = Name;

    public required string Name { get; init; }

    public ICollection<Lesson>? Lessons { get; init; }
    public ICollection<Record>? Records { get; init; }
}
