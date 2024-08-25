using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class RankExtensions
{
    public static RankDto toDTO(this Rank entity)
    {
        return new RankDto(entity.Id, entity.Name);
    }
}