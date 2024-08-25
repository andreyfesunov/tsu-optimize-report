namespace Tsu.IndividualPlan.WebApi.Dto.Event;

public class EventUpdateDto
{
    public EventUpdateDto(Guid Id, DateTime StartedAt, DateTime EndedAt)
    {
        this.Id = Id;
        this.StartedAt = StartedAt;
        this.EndedAt = EndedAt;
    }

    public Guid Id { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime EndedAt { get; init; }
}