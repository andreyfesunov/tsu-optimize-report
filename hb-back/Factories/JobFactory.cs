using BackendBase.Models;

namespace BackendBase.Factories;

public static class JobFactory
{
    public static List<Job> Make()
    {
        return new List<Job>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Доцент"
            }
        };
    }
}