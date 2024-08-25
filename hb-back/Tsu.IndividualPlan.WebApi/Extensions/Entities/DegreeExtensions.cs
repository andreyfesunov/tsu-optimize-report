using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class DegreeExtensions
{
    public static DegreeDto toDTO(this Degree entity)
    {
        return new DegreeDto(entity.Id, entity.Name);
    }
}