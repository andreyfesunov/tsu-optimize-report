namespace BackendBase.Dto.Lesson;

public class LessonUpdateDto
{
    public LessonUpdateDto(Guid Id, int? PlanDate, int? FactDate)
    {
        this.Id = Id;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public Guid Id { get; init; }
    public int? PlanDate { get; init; }
    public int? FactDate { get; init; }
}
