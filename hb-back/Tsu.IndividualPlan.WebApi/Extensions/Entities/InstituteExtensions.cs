using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class InstituteExtensions
{
    public static InstituteDto toDTO(this Institute entity)
    {
        return new InstituteDto(entity.Id, entity.Name);
    }
}