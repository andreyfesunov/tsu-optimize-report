using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class EventExtensions
{
    public static EventDto toDTO(this Event entity) =>
        new EventDto(
            Id: entity.Id,
            EventType: entity.EventType?.toDTO(),
            StartedAt: entity.StartedAt,
            EndedAt: entity.EndedAt,
            Lessons: entity.Lessons?.Select(x => x.toDTO()).ToList(),
            Comments: entity.Comments?.Select(x => x.toDTO()).ToList()
        );
}
