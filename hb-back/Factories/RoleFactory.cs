using BackendBase.Models;

namespace BackendBase.Factories;

public static class RoleFactory
{
    public static IEnumerable<Role> Make()
    {
        return new List<Role>
        {
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "USER"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "SUPERADMIN"
            }
        };
    }
}