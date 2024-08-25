using Tsu.IndividualPlan.WebApi.Models.Enum;

namespace Tsu.IndividualPlan.WebApi.Dto.Report;

public class ReportListDto
{
    public ReportListDto(Guid Id, double Rate, StateUserStatus Status, StateDto? State)
    {
        this.Id = Id;
        this.Rate = Rate;
        this.Status = Status;
        this.State = State;
    }

    public Guid Id { get; init; }
    public double Rate { get; init; }
    public StateUserStatus Status { get; init; }

    public StateDto? State { get; init; }
}