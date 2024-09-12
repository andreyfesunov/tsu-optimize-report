namespace Tsu.IndividualPlan.WebApi.Dto;

public class LessonDto(Guid Id, LessonTypeDto? LessonType, int? FactDate, int? PlanDate)
{
    public Guid Id { get; init; } = Id;
    public LessonTypeDto? LessonType { get; init; } = LessonType;
    public int? FactDate { get; init; } = FactDate;
    public int? PlanDate { get; init; } = PlanDate;
}