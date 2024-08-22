using BackendBase.Extensions;
using BackendBase.Models;

namespace BackendBase.Factories;

public static class StateFactory
{
    public static List<State> Make(Department department, Job job) =>
        new List<State>
        {
            new(
                Hours: 1485,
                Count: 1,
                EndDate: new DateTime(2024, 5, 31).SetKindUtc(),
                StartDate: new DateTime(2024, 2, 1).SetKindUtc(),
                DepartmentId: department.Id,
                JobId: job.Id
            )
        };
}
