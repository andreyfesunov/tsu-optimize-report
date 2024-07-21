namespace BackendBase.Models;

public class LessonType : Base
{
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<Record> Records { get; set; }

    public string Name { get; set; }
}