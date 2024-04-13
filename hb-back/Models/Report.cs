namespace BackendBase.Models;

public class Report : Base
{
    public File File { get; set; }
    public StateUser StateUser { get; set; }
    public ICollection<Event> Events { get; set; }
}