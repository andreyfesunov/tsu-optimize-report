namespace Tsu.IndividualPlan.WebApi.Dto.Comment;

public class CommentUpdateDto
{
    public CommentUpdateDto(Guid Id, string Content, int? PlanDate, int? FactDate)
    {
        this.Id = Id;
        this.Content = Content;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public Guid Id { get; init; }
    public string Content { get; init; }
    public int? PlanDate { get; init; }
    public int? FactDate { get; init; }
}