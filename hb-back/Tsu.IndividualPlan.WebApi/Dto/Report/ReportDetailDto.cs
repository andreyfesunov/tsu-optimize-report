using Tsu.IndividualPlan.WebApi.Models.Enum;

namespace Tsu.IndividualPlan.WebApi.Dto.Report;

public class ReportDetailDto : ReportListDto
{
    public ReportDetailDto(
        Guid Id,
        double Rate,
        StateUserStatus Status,
        StateDto? State,
        ICollection<EventDto>? Events = null
    )
        : base(Id, Rate, Status, State)
    {
        this.Events = Events ?? new List<EventDto>();
    }

    public ICollection<EventDto> Events { get; init; }
}