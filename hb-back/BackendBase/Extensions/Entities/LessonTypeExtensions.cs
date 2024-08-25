using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class LessonTypeExtensions
{
    public static LessonTypeDto toDTO(this LessonType entity) =>
        new LessonTypeDto(Id: entity.Id, Name: entity.Name);
}
