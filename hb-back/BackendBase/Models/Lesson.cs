namespace BackendBase.Models;

public class Lesson : Base
{
    public Guid EventId { get; set; }
    public Event Event { get; set; }

    public Guid LessonTypeId { get; set; }
    public LessonType LessonType { get; set; }

    public int? FactDate { get; set; }
    public int? PlanDate { get; set; }
}