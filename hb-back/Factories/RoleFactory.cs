using BackendBase.Models;

namespace BackendBase.Factories;

public static class RoleFactory
{
    public static List<Role> Make()
    {
        return new List<Role>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "USER"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "SUPERADMIN"
            }
        };
    }
}