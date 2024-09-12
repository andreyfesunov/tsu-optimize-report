namespace Tsu.IndividualPlan.WebApi.Dto;

public class StateDto(
    Guid Id,
    int Count,
    int Hours,
    DateTime StartDate,
    DateTime EndDate,
    DepartmentDto? Department,
    JobDto? Job)
{
    public Guid Id { get; init; } = Id;
    public int Count { get; init; } = Count;
    public int Hours { get; init; } = Hours;

    public DateTime StartDate { get; init; } = StartDate;
    public DateTime EndDate { get; init; } = EndDate;

    public DepartmentDto? Department { get; init; } = Department;
    public JobDto? Job { get; init; } = Job;
}