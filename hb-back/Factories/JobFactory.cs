using BackendBase.Models;

namespace BackendBase.Factories;

public static class JobFactory
{
    public static IEnumerable<Job> Make()
    {
        return new List<Job>
        {
            new Job
            {
                Id = Guid.NewGuid(),
                Name = "Доцент"
            }
        };
    }
}