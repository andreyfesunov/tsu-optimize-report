using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class RecordExtensions
{
    public static RecordDto toDTO(this Record entity)
    {
        return new RecordDto(
            entity.Id,
            entity.Hours,
            entity.Activity?.toDTO(),
            entity.StateUser?.toListDTO(),
            entity.LessonType?.toDTO()
        );
    }
}