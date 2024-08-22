namespace BackendBase.Dto.Comment;

public class CommentUpdateDto
{
    public CommentUpdateDto(Guid Id, string Content, int? PlanDate, int? FactDate)
    {
        this.Id = Id;
        this.Content = Content;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public readonly Guid Id;
    public readonly string Content;
    public readonly int? PlanDate;
    public readonly int? FactDate;
}
