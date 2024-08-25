namespace Tsu.IndividualPlan.WebApi.Dto.Comment;

public class CommentCreateDto
{
    public CommentCreateDto(Guid EventId, string Content, int? PlanDate, int? FactDate)
    {
        this.EventId = EventId;
        this.Content = Content;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public Guid EventId { get; init; }
    public string Content { get; init; }
    public int? PlanDate { get; init; }
    public int? FactDate { get; init; }
}