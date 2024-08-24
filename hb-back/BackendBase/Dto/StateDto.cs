namespace BackendBase.Dto;

public class StateDto
{
    public StateDto(
        Guid Id,
        int Count,
        int Hours,
        DateTime StartDate,
        DateTime EndDate,
        DepartmentDto? Department,
        JobDto? Job
    )
    {
        this.Id = Id;
        this.Count = Count;
        this.Hours = Hours;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.Department = Department;
        this.Job = Job;
    }

    public Guid Id { get; init; }
    public int Count { get; init; }
    public int Hours { get; init; }

    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public DepartmentDto? Department { get; init; }
    public JobDto? Job { get; init; }
}
