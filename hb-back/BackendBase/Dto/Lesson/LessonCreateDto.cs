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

    public readonly Guid EventId;
    public readonly Guid LessonTypeId;
    public readonly int? PlanDate;
    public readonly int? FactDate;
}
