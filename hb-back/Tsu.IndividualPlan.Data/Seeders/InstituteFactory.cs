using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Seeders;

public static class InstituteFactory
{
    public static List<Institute> Make()
    {
        return new List<Institute> { new("Институт прикладной математики и компьютерных наук") };
    }
}