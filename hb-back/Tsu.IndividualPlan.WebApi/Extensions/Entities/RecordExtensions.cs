using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

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