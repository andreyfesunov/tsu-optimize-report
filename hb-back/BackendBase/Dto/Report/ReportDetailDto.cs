using BackendBase.Models.Enum;

namespace BackendBase.Dto.Report;

public class ReportDetailDto : ReportListDto
{
    public ReportDetailDto(
        Guid Id,
        double Rate,
        StateUserStatus Status,
        StateDto? State,
        ICollection<EventDto>? Events = null
    )
        : base(Id: Id, Rate: Rate, Status: Status, State: State) =>
        this.Events = Events ?? new List<EventDto>();

    public readonly ICollection<EventDto> Events;
}
