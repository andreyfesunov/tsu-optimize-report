using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class DepartmentExtensions
{
    public static DepartmentDto toDTO(this Department entity)
    {
        return new DepartmentDto(entity.Id, entity.Name, entity.Institute?.toDTO());
    }
}