namespace BackendBase.Dto;

public class LessonDto
{
    public LessonDto(Guid Id, LessonTypeDto? LessonType, int? FactDate, int? PlanDate)
    {
        this.Id = Id;
        this.LessonType = LessonType;
        this.PlanDate = PlanDate;
        this.FactDate = FactDate;
    }

    public Guid Id { get; init; }
    public LessonTypeDto? LessonType { get; init; }
    public int? FactDate { get; init; }
    public int? PlanDate { get; init; }
}
