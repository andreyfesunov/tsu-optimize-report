using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Factories;

public static class JobFactory
{
    public static List<Job> Make()
    {
        return new List<Job>() { new("Доцент") };
    }
}