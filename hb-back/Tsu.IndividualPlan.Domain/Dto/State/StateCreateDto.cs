namespace Tsu.IndividualPlan.Domain.Dto.State;

public class StateCreateDto(
    Guid JobId,
    DateTime StartDate,
    DateTime EndDate,
    Guid DepartmentId,
    int Count = 1,
    int Hours = 1485)
{
    public int Count { get; init; } = Count;
    public int Hours { get; init; } = Hours;
    public Guid JobId { get; init; } = JobId;
    public DateTime StartDate { get; init; } = StartDate;
    public DateTime EndDate { get; init; } = EndDate;
    public Guid DepartmentId { get; init; } = DepartmentId;
}