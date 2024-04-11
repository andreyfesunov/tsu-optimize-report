namespace BackendBase.Models;

public class Report : Base
{
    public File File { get; set; }
    public User User { get; set; }
    public ICollection<Event> Events { get; set; }
}