namespace Tsu.IndividualPlan.Domain.Dto.Event;

public class EventUpdateDto(Guid Id, DateTime StartedAt, DateTime EndedAt)
{
    public Guid Id { get; init; } = Id;
    public DateTime StartedAt { get; init; } = StartedAt;
    public DateTime EndedAt { get; init; } = EndedAt;
}