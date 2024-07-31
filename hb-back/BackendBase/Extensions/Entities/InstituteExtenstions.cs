using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class InstituteExtensions
{
    public static InstituteDto toDTO(this Institute entity) => new InstituteDto
    {
        Id = entity.Id.ToString(),
        Name = entity.Name,
    };
}
