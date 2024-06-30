namespace BackendBase.Dto;

public class CommentDto
{
    public string Id { get; set; }
    public string Content { get; set; }
    public DateTime FactDate { get; set; }
    public DateTime PlanDate { get; set; }
}