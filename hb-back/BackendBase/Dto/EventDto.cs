namespace BackendBase.Dto;

public class EventDto
{
    public EventDto(
        Guid Id,
        EventTypeDto EventType,
        DateTime StartedAt,
        DateTime EndedAt,
        ICollection<LessonDto>? Lessons,
        ICollection<CommentDto>? Comments
    )
    {
        this.Id = Id;
        this.EventType = EventType;
        this.StartedAt = StartedAt;
        this.EndedAt = EndedAt;
        this.Lessons = Lessons ?? new List<LessonDto>();
        this.Comments = Comments ?? new List<CommentDto>();
    }

    public Guid Id { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime EndedAt { get; init; }

    public ICollection<LessonDto> Lessons { get; init; }
    public ICollection<CommentDto> Comments { get; init; }

    public EventTypeDto? EventType { get; init; }
}
