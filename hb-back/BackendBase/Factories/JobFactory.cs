using BackendBase.Models;

namespace BackendBase.Factories;

public static class JobFactory
{
    public static List<Job> Make() => new List<Job> { new(Name: "Доцент") };
}
