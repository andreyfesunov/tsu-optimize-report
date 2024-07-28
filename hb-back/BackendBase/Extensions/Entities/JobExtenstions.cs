using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class JobExtensions
{
    public static JobDto toDTO(this Job entity) => new JobDto
    {
        Id = entity.Id.ToString(),
        Name = entity.Name
    };
}
