using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class WorkExtensions
{
    public static WorkDto toDTO(this Work entity)
    {
        return new WorkDto(entity.Id, entity.Name);
    }
}