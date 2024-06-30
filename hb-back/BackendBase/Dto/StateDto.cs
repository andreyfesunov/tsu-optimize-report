namespace BackendBase.Dto;

public class StateDto
{
    public string Id { get; set; }
    public int Count { get; set; }
    public int Hours { get; set; }

    public DepartmentDto Department { get; set; }
    public JobDto Job { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}