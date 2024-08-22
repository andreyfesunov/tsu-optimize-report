namespace BackendBase.Dto.Lesson;

public class LessonUpdateDto
{
    public LessonUpdateDto(Guid Id, int? PlanDate, int? FactDate)
    {
        this.Id = Id;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public readonly Guid Id;
    public readonly int? PlanDate;
    public readonly int? FactDate;
}
