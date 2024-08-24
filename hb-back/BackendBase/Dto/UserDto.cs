using BackendBase.Models.Enum;

namespace BackendBase.Dto;

public class UserDto
{
    public UserDto(
        Guid Id,
        string Email,
        string Firstname,
        string Lastname,
        RoleUserEnum Role,
        RankDto? Rank,
        DegreeDto? Degree
    )
    {
        this.Id = Id;
        this.Email = Email;
        this.Firstname = Firstname;
        this.Lastname = Lastname;
        this.Role = Role;
        this.Rank = Rank;
        this.Degree = Degree;
    }

    public Guid Id { get; init; }
    public string Email { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public RoleUserEnum Role { get; init; }
    public RankDto? Rank { get; init; }
    public DegreeDto? Degree { get; init; }
}
