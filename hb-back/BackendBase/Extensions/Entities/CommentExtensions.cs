using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class CommentExtensions
{
    public static CommentDto toDTO(this Comment entity) =>
        new CommentDto(
            Id: entity.Id,
            Content: entity.Content,
            FactDate: entity.FactDate,
            PlanDate: entity.PlanDate
        );
}
