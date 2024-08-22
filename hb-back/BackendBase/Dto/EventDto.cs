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

    public readonly Guid Id;
    public readonly DateTime StartedAt;
    public readonly DateTime EndedAt;

    public readonly ICollection<LessonDto> Lessons;
    public readonly ICollection<CommentDto> Comments;

    public readonly EventTypeDto? EventType;
}
