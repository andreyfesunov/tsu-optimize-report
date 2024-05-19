namespace BackendBase.Models;

public class StateUser : Base
{
    public ICollection<Event> Events { get; set; }
    public ICollection<File> Files { get; set; }
    public ICollection<Record> Records { get; set; }
    public Guid StateId { get; set; }
    public Guid UserId { get; set; }
    public State State { get; set; }
    public User User { get; set; }
    public double Rate { get; set; }
}