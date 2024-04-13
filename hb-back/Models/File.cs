namespace BackendBase.Models;

public class File : Base
{
    ICollection<EventFile> EventsFiles { get; set; }
    public StateUser StateUser { get; set; }
    public string Path { get; set; }
    public DateTime CreatedDate { get; set; }
}  