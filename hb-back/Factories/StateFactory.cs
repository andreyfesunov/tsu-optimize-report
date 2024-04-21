using BackendBase.Models;

namespace BackendBase.Factories;

public static class StateFactory
{
    public static IEnumerable<State> Make(Department department, Job job)
    {
        return new List<State>
        {
            new State
            {
                Id = Guid.NewGuid(),
                Count = 1,
                Department = department,
                Job = job,
                Hours = 1485,
                EndDate = new DateTime(2024, 5, 31),
                StartDate = new DateTime(2024, 2, 1)
            }
        };
    }
}