namespace BackendBase.Models;

public class Lesson : Base
{
    public Event Event { get; set; }
    public string Name { get; set; }
    public DateTime FactDate { get; set; }
    public DateTime PlanDate { get; set; }
}