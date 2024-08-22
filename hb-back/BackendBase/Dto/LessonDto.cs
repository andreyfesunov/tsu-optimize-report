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

    public readonly Guid Id;
    public readonly LessonTypeDto? LessonType;
    public readonly int? FactDate;
    public readonly int? PlanDate;
}
