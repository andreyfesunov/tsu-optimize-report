﻿using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class Comment : Base
{
    protected Comment() { }

    [SetsRequiredMembers]
    public Comment(string Content, Guid EventId, Guid? Id = null)
        : base(Id)
    {
        this.Content = Content;
        this.EventId = EventId;
    }

    /** Editable Fields */
    public required string Content { get; set; }
    public int? FactDate { get; set; }
    public int? PlanDate { get; set; }

    /** Comment belongs to Event */
    public required Guid EventId { get; init; }
    public Event? Event { get; init; }
}
