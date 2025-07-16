using System.Diagnostics.CodeAnalysis;
using Tsu.IndividualPlan.Domain.Exceptions;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class Event : Base
{
    protected Event()
    {
    }

    [SetsRequiredMembers]
    public Event(
        Guid StateUserId,
        Guid EventTypeId,
        DateTime StartedAt,
        DateTime EndedAt,
        int SemestrId,
        Guid? Id = null
    )
        : base(Id)
    {
        this.StateUserId = StateUserId;
        this.EventTypeId = EventTypeId;
        this.StartedAt = StartedAt;
        this.EndedAt = EndedAt;
        this.SemestrId = SemestrId;
    }

    public required Guid StateUserId { get; init; }
    public required Guid EventTypeId { get; init; }

    /**
     * Editable Fields
     */
    public required DateTime StartedAt { get; set; }

    public required DateTime EndedAt { get; set; }
    public required int SemestrId { get; init; }

    public ICollection<Lesson>? Lessons { get; init; }
    public ICollection<Comment>? Comments { get; init; }

    public EventType? EventType { get; init; }
    public StateUser? StateUser { get; init; }

    /**
     * Methods
     */
    public virtual Guid? WorkId => EventType?.WorkId;

    public virtual bool HasLessonType(Guid Id)
    {
        if (Lessons == null) throw new AppException("Lessons not loaded");
        return Lessons.Any(x => x.Id == Id);
    }
}