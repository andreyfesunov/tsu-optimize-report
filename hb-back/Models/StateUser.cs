namespace BackendBase.Models;

public class StateUser : Base
{
    public ICollection<Event> Events { get; set; }
    public ICollection<File> Files { get; set; }
    public State State { get; set; }
    public User User { get; set; }
    public double Rate { get; set; }
    /** Later enum */
    public string Status { get; set; }
} 