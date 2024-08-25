using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class LessonExtensions
{
    public static LessonDto toDTO(this Lesson entity) =>
        new LessonDto(
            Id: entity.Id,
            LessonType: entity.LessonType?.toDTO(),
            FactDate: entity.FactDate,
            PlanDate: entity.PlanDate
        );
}
