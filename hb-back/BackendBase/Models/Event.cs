﻿using System.Diagnostics.CodeAnalysis;
using BackendBase.Exceptions;

namespace BackendBase.Models;

public class Event : Base
{
    protected Event() { }

    [SetsRequiredMembers]
    public Event(
        Guid StateUserId,
        Guid EventTypeId,
        DateTime StartedAt,
        DateTime EndedAt,
        Guid? Id = null
    )
        : base(Id)
    {
        this.StateUserId = StateUserId;
        this.EventTypeId = EventTypeId;
        this.StartedAt = StartedAt;
        this.EndedAt = EndedAt;
    }

    public required Guid StateUserId { get; init; }
    public required Guid EventTypeId { get; init; }

    /** Editable Fields */
    public required DateTime StartedAt { get; set; }
    public required DateTime EndedAt { get; set; }

    public ICollection<Lesson>? Lessons { get; init; }
    public ICollection<Comment>? Comments { get; init; }
    public ICollection<EventFile>? EventsFiles { get; init; }

    public EventType? EventType { get; init; }
    public StateUser? StateUser { get; init; }

    /** Methods */
    public virtual Guid? WorkId => this.EventType?.WorkId;

    public virtual bool HasLessonType(Guid Id)
    {
        if (this.Lessons == null)
        {
            throw new AppException("Lessons not loaded");
        }
        return this.Lessons.Where(x => x.Id == Id).Count() != 0;
    }
}
