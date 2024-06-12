namespace BackendBase.Dto.Lesson;

public class LessonCreateDto
{
    public string EventId { get; set; }
    public string LessonTypeId { get; set; }
    public int? PlanDate { get; set; }
    public int? FactDate { get; set; }
}