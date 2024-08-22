using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class DegreeExtensions
{
    public static DegreeDto toDTO(this Degree entity) =>
        new DegreeDto(Id: entity.Id, Name: entity.Name);
}
