using Tsu.IndividualPlan.Domain.Enumerations;

namespace Tsu.IndividualPlan.WebApi.Dto.Report;

public class ReportDetailDto(
    Guid Id,
    double Rate,
    StateUserStatus Status,
    StateDto? State,
    ICollection<EventDto>? Events = null)
    : ReportListDto(Id, Rate, Status, State)
{
    public ICollection<EventDto>? Events { get; init; } = Events;
}