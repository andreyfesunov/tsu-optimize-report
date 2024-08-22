using BackendBase.Models;

namespace BackendBase.Factories;

public static class InstituteFactory
{
    public static List<Institute> Make() =>
        new List<Institute> { new(Name: "Институт прикладной математики и компьютерных наук") };
}
