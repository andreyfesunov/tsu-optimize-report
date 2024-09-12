using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class EventTypeExtensions
{
    public static EventTypeDto toDTO(this EventType entity)
    {
        return new EventTypeDto(
            entity.Id,
            entity.Name,
            Work: entity.Work?.toDTO(),
            Description: entity.Description
        );
    }
}