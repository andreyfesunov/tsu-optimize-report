using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto.Report;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class IndividualPlanDto
{
    public static ReportListDto toListDTO(this StateUser entity)
    {
        return new ReportListDto(
            entity.Id,
            entity.Rate,
            State: entity.State?.toDTO(),
            Status: entity.Status
        );
    }

    public static ReportDetailDto toDTO(this StateUser entity)
    {
        return new ReportDetailDto(
            entity.Id,
            entity.Rate,
            State: entity.State?.toDTO(),
            Status: entity.Status,
            Events: entity.Events?.Select(x => x.toDTO()).ToList()
        );
    }
}