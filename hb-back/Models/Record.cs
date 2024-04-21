namespace BackendBase.Models;

public class Record : Base
{
    public Activity Activity { get; set; }
    public StateUser StateUser { get; set; }
    public LessonType LessonType { get; set; }
    public int Hours { get; set; }
}