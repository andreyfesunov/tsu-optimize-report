using BackendBase.Models;

namespace BackendBase.Factories;

public static class RoleUserFactory
{
    public static List<RoleUser> Make(List<Role> roles, List<User> user)
    {
        return new List<RoleUser>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Role = roles.First(),
                User = user.First(),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = roles.Last(),
                User = user.First(),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = roles.First(),
                User = user.Last(),
            }
        };
    }
}