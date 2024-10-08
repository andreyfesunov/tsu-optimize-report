using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class LessonTypeExtensions
{
    public static LessonTypeDto toDTO(this LessonType entity)
    {
        return new LessonTypeDto(entity.Id, entity.Name);
    }
}