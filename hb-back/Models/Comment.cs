namespace BackendBase.Models;

public class Comment : Base
{
    public Guid EventId { get; set; }
    public Event Event { get; set; }

    public string Content { get; set; }

    public int? FactDate { get; set; }
    public int? PlanDate { get; set; }
}