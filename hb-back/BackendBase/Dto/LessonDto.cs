namespace BackendBase.Dto;

public class LessonDto
{
    public string Id { get; set; }
    public LessonTypeDto LessonType { get; set; }
    public int? FactDate { get; set; }
    public int? PlanDate { get; set; }
}