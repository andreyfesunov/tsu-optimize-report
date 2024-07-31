using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class UserExtensions
{
    public static UserDto toDTO(this User entity) => new UserDto
    {
        Id = entity.Id.ToString(),
        Rank = entity.Rank,
        Role = entity.Role,
        Email = entity.Email,
        Degree = entity.Degree,
        Lastname = entity.Lastname,
        Firstname = entity.Firstname
    };
}
