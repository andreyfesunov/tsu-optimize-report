using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Factories;

public static class InstituteFactory
{
    public static List<Institute> Make()
    {
        return new List<Institute>() { new("Институт прикладной математики и компьютерных наук") };
    }
}