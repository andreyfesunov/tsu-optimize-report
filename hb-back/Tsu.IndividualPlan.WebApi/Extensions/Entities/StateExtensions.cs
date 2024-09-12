using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class StateExtensions
{
    public static StateDto toDTO(this State entity)
    {
        return new StateDto(
            entity.Id,
            entity.Count,
            EndDate: entity.EndDate,
            StartDate: entity.StartDate,
            Hours: entity.Hours,
            Job: entity.Job?.toDTO(),
            Department: entity.Department?.toDTO()
        );
    }
}