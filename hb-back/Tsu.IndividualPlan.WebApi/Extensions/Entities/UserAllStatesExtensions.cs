using Tsu.IndividualPlan.Domain.Models;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class UserAllStatesExtensions
{
    public static UserAllStatesDto toDTO(this UserAllStates entity)
    {
        return new UserAllStatesDto(
            entity.User.toDTO(),
            entity.States.Select(s => s.toDTO())
        );
    }
}