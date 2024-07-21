namespace BackendBase.Dto.Comment;

public class CommentUpdateDto
{
    public string Id { get; set; }
    public string Content { get; set; }
    public int? PlanDate { get; set; }
    public int? FactDate { get; set; }
}