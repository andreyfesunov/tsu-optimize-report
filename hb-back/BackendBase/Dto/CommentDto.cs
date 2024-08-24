namespace BackendBase.Dto;

public class CommentDto
{
    public CommentDto(Guid Id, string Content, DateTime PlanDate, DateTime FactDate)
    {
        this.Id = Id;
        this.Content = Content;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public Guid Id { get; init; }
    public string Content { get; init; }
    public DateTime FactDate { get; init; }
    public DateTime PlanDate { get; init; }
}
