namespace BackendBase.Dto.Lesson;

public class LessonCreateDto
{
    public LessonCreateDto(Guid EventId, Guid LessonTypeId, int? PlanDate, int? FactDate)
    {
        this.EventId = EventId;
        this.LessonTypeId = LessonTypeId;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public Guid EventId { get; init; }
    public Guid LessonTypeId { get; init; }
    public int? PlanDate { get; init; }
    public int? FactDate { get; init; }
}
