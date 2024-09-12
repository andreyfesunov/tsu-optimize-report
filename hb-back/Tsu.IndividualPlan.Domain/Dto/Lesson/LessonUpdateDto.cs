namespace Tsu.IndividualPlan.Domain.Dto.Lesson;

public class LessonUpdateDto(Guid Id, int? PlanDate, int? FactDate)
{
    public Guid Id { get; init; } = Id;
    public int? PlanDate { get; init; } = PlanDate;
    public int? FactDate { get; init; } = FactDate;
}