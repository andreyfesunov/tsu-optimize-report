namespace BackendBase.Dto.Comment;

public class CommentCreateDto
{
    public CommentCreateDto(Guid EventId, string Content, int? PlanDate, int? FactDate)
    {
        this.EventId = EventId;
        this.Content = Content;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public readonly Guid EventId;
    public readonly string Content;
    public readonly int? PlanDate;
    public readonly int? FactDate;
}
