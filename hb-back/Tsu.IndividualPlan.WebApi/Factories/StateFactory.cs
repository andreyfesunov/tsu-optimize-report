using Tsu.IndividualPlan.WebApi.Extensions;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Factories;

public static class StateFactory
{
    public static List<State> Make(Department department, Job job)
    {
        return new List<State>()
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
}