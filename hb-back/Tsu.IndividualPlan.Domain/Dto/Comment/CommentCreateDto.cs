namespace Tsu.IndividualPlan.Domain.Dto.Comment;

public class CommentCreateDto(Guid EventId, string Content, int? PlanDate, int? FactDate)
{
    public Guid EventId { get; init; } = EventId;
    public string Content { get; init; } = Content;
    public int? PlanDate { get; init; } = PlanDate;
    public int? FactDate { get; init; } = FactDate;
}