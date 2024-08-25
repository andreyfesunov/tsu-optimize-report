using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class WorkExtensions
{
    public static WorkDto toDTO(this Work entity)
    {
        return new WorkDto(entity.Id, entity.Name);
    }
}