namespace BackendBase.Models;

public class Lesson : Base
{
    public string Name { get; set; }
    public DateTime FactDate { get; set; }
    public DateTime PlanDate { get; set; }
}