using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class StateExtensions
{
    public static StateDto toDTO(this State entity) => new StateDto
    {
        Id = entity.Id.ToString(),
        Count = entity.Count,
        EndDate = entity.EndDate,
        StartDate = entity.StartDate,
        Hours = entity.Hours,
        Job = entity.Job.toDTO(),
        Department = entity.Department.toDTO()
    };
}
