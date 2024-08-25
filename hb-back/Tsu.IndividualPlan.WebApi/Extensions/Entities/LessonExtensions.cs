using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class LessonExtensions
{
    public static LessonDto toDTO(this Lesson entity)
    {
        return new LessonDto(
            entity.Id,
            entity.LessonType?.toDTO(),
            entity.FactDate,
            entity.PlanDate
        );
    }
}