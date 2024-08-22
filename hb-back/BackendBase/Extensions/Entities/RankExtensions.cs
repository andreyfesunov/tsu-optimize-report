using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class RankExtensions
{
    public static RankDto toDTO(this Rank entity) => new RankDto(Id: entity.Id, Name: entity.Name);
}
