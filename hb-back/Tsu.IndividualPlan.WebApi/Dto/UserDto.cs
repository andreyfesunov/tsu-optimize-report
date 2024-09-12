using Tsu.IndividualPlan.Domain.Enumerations;

namespace Tsu.IndividualPlan.WebApi.Dto;

public class UserDto(
    Guid Id,
    string Email,
    string Firstname,
    string Lastname,
    RoleUserEnum Role,
    RankDto? Rank,
    DegreeDto? Degree)
{
    public Guid Id { get; init; } = Id;
    public string Email { get; init; } = Email;
    public string Firstname { get; init; } = Firstname;
    public string Lastname { get; init; } = Lastname;
    public RoleUserEnum Role { get; init; } = Role;
    public RankDto? Rank { get; init; } = Rank;
    public DegreeDto? Degree { get; init; } = Degree;
}