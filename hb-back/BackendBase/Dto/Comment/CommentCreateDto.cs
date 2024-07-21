namespace BackendBase.Dto.Comment;

public class CommentCreateDto
{
    public string EventId { get; set; }
    public string Content { get; set; }
    public int? PlanDate { get; set; }
    public int? FactDate { get; set; }
}