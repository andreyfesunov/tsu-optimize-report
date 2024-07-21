namespace BackendBase.Dto;

public class EventDto
{
    public string Id { get; set; }
    public EventTypeDto EventType { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }

    public ICollection<LessonDto> Lessons { get; set; }
    public ICollection<CommentDto> Comments { get; set; }
}