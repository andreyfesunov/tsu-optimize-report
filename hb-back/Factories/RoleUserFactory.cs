using BackendBase.Models;

namespace BackendBase.Factories;

public static class RoleUserFactory
{
    public static IEnumerable<RoleUser> Make(IEnumerable<Role> roles, IEnumerable<User> user)
    {
        return new List<RoleUser>
        {
            new RoleUser
            {
                Id = Guid.NewGuid(),
                Role = roles.First(),
                User = user.First(),
            },
            new RoleUser
            {
                Id = Guid.NewGuid(),
                Role = roles.Last(),
                User = user.First()
            },
            new RoleUser
            {
                Id = Guid.NewGuid(),
                Role = roles.First(),
                User = user.Last()
            }
        };
    }
}