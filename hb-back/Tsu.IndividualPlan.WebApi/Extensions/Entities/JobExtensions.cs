using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class JobExtensions
{
    public static JobDto toDTO(this Job entity)
    {
        return new JobDto(entity.Id, entity.Name);
    }
}