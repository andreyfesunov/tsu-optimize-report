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

    public readonly Guid Id;
    public readonly string Content;
    public readonly DateTime FactDate;
    public readonly DateTime PlanDate;
}
