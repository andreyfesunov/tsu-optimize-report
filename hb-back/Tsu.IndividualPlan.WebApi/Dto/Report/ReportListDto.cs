using Tsu.IndividualPlan.Domain.Enumerations;

namespace Tsu.IndividualPlan.WebApi.Dto.Report;

public class ReportListDto(Guid Id, double Rate, StateUserStatus Status, StateDto? State)
{
    public Guid Id { get; init; } = Id;
    public double Rate { get; init; } = Rate;
    public StateUserStatus Status { get; init; } = Status;
    public StateDto? State { get; init; } = State;
}