using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Seeders;

public static class JobFactory
{
    public static List<Job> Make()
    {
        return new List<Job> { new("Доцент") };
    }
}