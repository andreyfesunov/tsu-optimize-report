using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class RecordExtensions
{
    public static RecordDto toDTO(this Record entity) =>
        new RecordDto(
            Id: entity.Id,
            Hours: entity.Hours,
            Activity: entity.Activity?.toDTO(),
            StateUser: entity.StateUser?.toListDTO(),
            LessonType: entity.LessonType?.toDTO()
        );
}
