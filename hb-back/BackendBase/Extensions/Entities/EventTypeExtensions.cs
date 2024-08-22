using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class EventTypeExtensions
{
    public static EventTypeDto toDTO(this EventType entity) =>
        new EventTypeDto(
            Id: entity.Id,
            Name: entity.Name,
            Work: entity.Work?.toDTO(),
            Description: entity.Description
        );
}
