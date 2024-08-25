using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class JobExtensions
{
    public static JobDto toDTO(this Job entity)
    {
        return new JobDto(entity.Id, entity.Name);
    }
}