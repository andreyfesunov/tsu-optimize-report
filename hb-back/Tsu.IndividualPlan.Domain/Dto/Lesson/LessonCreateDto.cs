namespace Tsu.IndividualPlan.Domain.Dto.Lesson;

public class LessonCreateDto(Guid EventId, Guid LessonTypeId, int? PlanDate, int? FactDate)
{
    public Guid EventId { get; init; } = EventId;
    public Guid LessonTypeId { get; init; } = LessonTypeId;
    public int? PlanDate { get; init; } = PlanDate;
    public int? FactDate { get; init; } = FactDate;
}