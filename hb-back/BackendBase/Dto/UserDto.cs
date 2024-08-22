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

    public readonly Guid Id;
    public readonly string Email;
    public readonly string Firstname;
    public readonly string Lastname;
    public readonly RoleUserEnum Role;
    public readonly RankDto? Rank;
    public readonly DegreeDto? Degree;
}
