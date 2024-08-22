using System.Diagnostics.CodeAnalysis;
using BackendBase.Models.Enum;

namespace BackendBase.Models;

public class User : Base
{
    protected User() { }

    [SetsRequiredMembers]
    public User(
        string Email,
        string Password,
        RoleUserEnum Role,
        string Firstname,
        string Lastname,
        Guid? RankId = null,
        Guid? DegreeId = null,
        Guid? Id = null
    )
        : base(Id)
    {
        this.Email = Email;
        this.Password = Password;
        this.Role = Role;
        this.Firstname = Firstname;
        this.Lastname = Lastname;
        this.RankId = RankId;
        this.DegreeId = DegreeId;
    }

    public required string Email { get; init; }
    public required string Password { get; init; }
    public required RoleUserEnum Role { get; init; }
    public required string Firstname { get; init; }
    public required string Lastname { get; init; }

    public Guid? RankId { get; private set; }
    public Guid? DegreeId { get; private set; }

    public Rank? Rank { get; private set; }
    public Degree? Degree { get; private set; }
}
