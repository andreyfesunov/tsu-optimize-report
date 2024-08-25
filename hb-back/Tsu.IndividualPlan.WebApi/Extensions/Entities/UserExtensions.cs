using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class UserExtensions
{
    public static UserDto toDTO(this User entity)
    {
        return new UserDto(
            entity.Id,
            Rank: entity.Rank?.toDTO(),
            Role: entity.Role,
            Email: entity.Email,
            Degree: entity.Degree?.toDTO(),
            Lastname: entity.Lastname,
            Firstname: entity.Firstname
        );
    }
}