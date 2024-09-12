namespace Tsu.IndividualPlan.Domain.Dto.Comment;

public class CommentUpdateDto(Guid Id, string Content, int? PlanDate, int? FactDate)
{
    public Guid Id { get; init; } = Id;
    public string Content { get; init; } = Content;
    public int? PlanDate { get; init; } = PlanDate;
    public int? FactDate { get; init; } = FactDate;
}