using BackendBase.Models;

namespace BackendBase.Factories;

public static class InstituteFactory
{
    public static List<Institute> Make()
    {
        return new List<Institute>
        {
            new Institute()
            {
                Id = Guid.NewGuid(),
                Name = "Институт прикладной математики и компьютерных наук"
            }
        };
    }
}