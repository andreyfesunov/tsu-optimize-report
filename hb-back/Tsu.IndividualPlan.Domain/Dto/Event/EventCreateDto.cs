﻿namespace Tsu.IndividualPlan.Domain.Dto.Event;

public class EventCreateDto(Guid StateUserId, Guid EventTypeId, DateTime StartedAt, DateTime EndedAt)
{
    public Guid EventTypeId { get; init; } = EventTypeId;
    public Guid StateUserId { get; init; } = StateUserId;
    public DateTime StartedAt { get; init; } = StartedAt;
    public DateTime EndedAt { get; init; } = EndedAt;
}