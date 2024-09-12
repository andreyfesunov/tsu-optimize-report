using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class EventExtensions
{
    public static EventDto toDTO(this Event entity)
    {
        return new EventDto(
            entity.Id,
            entity.EventType?.toDTO(),
            entity.StartedAt,
            entity.EndedAt,
            entity.Lessons?.Select(x => x.toDTO()).ToList(),
            entity.Comments?.Select(x => x.toDTO()).ToList()
        );
    }
}