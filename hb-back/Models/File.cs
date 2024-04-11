namespace BackendBase.Models;

public class File : Base
{
    public User User { get; set; }
    public string Path { get; set; }
}