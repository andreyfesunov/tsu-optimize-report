namespace Tsu.IndividualPlan.WebApi.Dto;

public class CommentDto(Guid Id, string Content, int? PlanDate, int? FactDate)
{
    public Guid Id { get; init; } = Id;
    public string Content { get; init; } = Content;
    public int? FactDate { get; init; } = FactDate;
    public int? PlanDate { get; init; } = PlanDate;
}