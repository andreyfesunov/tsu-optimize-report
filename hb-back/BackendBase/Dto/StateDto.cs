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

    public readonly Guid Id;
    public readonly int Count;
    public readonly int Hours;

    public readonly DateTime StartDate;
    public readonly DateTime EndDate;

    public readonly DepartmentDto? Department;
    public readonly JobDto? Job;
}
