namespace Tsu.IndividualPlan.WebApi.Dto;

public class EventDto(
    Guid Id,
    EventTypeDto? EventType,
    DateTime StartedAt,
    DateTime EndedAt,
    ICollection<LessonDto>? Lessons,
    ICollection<CommentDto>? Comments)
{
    public Guid Id { get; init; } = Id;
    public DateTime StartedAt { get; init; } = StartedAt;
    public DateTime EndedAt { get; init; } = EndedAt;

    public ICollection<LessonDto>? Lessons { get; init; } = Lessons;
    public ICollection<CommentDto>? Comments { get; init; } = Comments;

    public EventTypeDto? EventType { get; init; } = EventType;
}