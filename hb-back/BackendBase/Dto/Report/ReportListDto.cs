using BackendBase.Models.Enum;

namespace BackendBase.Dto.Report;

public class ReportListDto
{
    public ReportListDto(Guid Id, double Rate, StateUserStatus Status, StateDto? State)
    {
        this.Id = Id;
        this.Rate = Rate;
        this.Status = Status;
        this.State = State;
    }

    public readonly Guid Id;
    public readonly double Rate;
    public readonly StateUserStatus Status;

    public readonly StateDto? State;
}
