using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class DepartmentExtensions
{
    public static DepartmentDto toDTO(this Department entity)
    {
        return new DepartmentDto(entity.Id, entity.Name, entity.Institute?.toDTO());
    }
}