namespace BackendBase.Models;

public class Record : Base
{
    public Guid ActivityId { get; set; }
    public Activity Activity { get; set; }
    public Guid StateUserId { get; set; }
    public StateUser StateUser { get; set; }
    public Guid LessonTypeId { get; set; }
    public LessonType LessonType { get; set; }
    public int Hours { get; set; }
}