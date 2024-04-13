namespace BackendBase.Models;

public class StateUser : Base
{
    public User User { get; set; }
    public State State { get; set; }
    public double Rate { get; set; }
    /** Later enum */
    public string Status { get; set; }
}