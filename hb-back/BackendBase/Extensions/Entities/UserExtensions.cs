using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class UserExtensions
{
    public static UserDto toDTO(this User entity) =>
        new UserDto(
            Id: entity.Id,
            Rank: entity.Rank?.toDTO(),
            Role: entity.Role,
            Email: entity.Email,
            Degree: entity.Degree?.toDTO(),
            Lastname: entity.Lastname,
            Firstname: entity.Firstname
        );
}
