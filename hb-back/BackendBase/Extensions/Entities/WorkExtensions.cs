using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class WorkExtensions
{
    public static WorkDto toDTO(this Work entity) => new WorkDto(Id: entity.Id, Name: entity.Name);
}
