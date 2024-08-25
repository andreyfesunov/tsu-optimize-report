using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class CommentExtensions
{
    public static CommentDto toDTO(this Comment entity)
    {
        return new CommentDto(
            entity.Id,
            entity.Content,
            FactDate: entity.FactDate,
            PlanDate: entity.PlanDate
        );
    }
}