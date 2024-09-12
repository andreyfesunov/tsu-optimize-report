using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class DegreeExtensions
{
    public static DegreeDto toDTO(this Degree entity)
    {
        return new DegreeDto(entity.Id, entity.Name);
    }
}