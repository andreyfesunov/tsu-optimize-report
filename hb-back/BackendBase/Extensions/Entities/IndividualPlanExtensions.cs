using BackendBase.Dto.Report;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class IndividualPlanDto
{
    public static ReportListDto toListDTO(this StateUser entity) =>
        new ReportListDto(
            Id: entity.Id,
            Rate: entity.Rate,
            State: entity.State?.toDTO(),
            Status: entity.Status
        );

    public static ReportDetailDto toDTO(this StateUser entity) =>
        new ReportDetailDto(
            Id: entity.Id,
            Rate: entity.Rate,
            State: entity.State?.toDTO(),
            Status: entity.Status,
            Events: entity.Events?.Select(x => x.toDTO()).ToList()
        );
}
