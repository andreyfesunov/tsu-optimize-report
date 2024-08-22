using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class DepartmentExtensions
{
    public static DepartmentDto toDTO(this Department entity) =>
        new DepartmentDto(Id: entity.Id, Name: entity.Name, Institute: entity.Institute?.toDTO());
}
