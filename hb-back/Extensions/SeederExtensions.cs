using BackendBase.Data;
using BackendBase.Factories;

namespace BackendBase.Extensions;

public static class SeederExtensions
{
    public static void Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        var activity = context.Activities.FirstOrDefault();

        if (activity != null) return;

        var institutes = InstituteFactory.Make();
        var departments = DepartmentFactory.Make(institutes.First());
        var jobs = JobFactory.Make();
        var users = UserFactory.Make();
        var states = StateFactory.Make(departments.First(), jobs.First());

        context.Activities.AddRange(ActivityFactory.Make());

        context.Institutes.AddRange(institutes);
        context.Departments.AddRange(departments);
        context.Jobs.AddRange(jobs);
        context.Users.AddRange(users);
        context.States.AddRange(states);

        context.StatesUsers.AddRange(StateUserFactory.Make(users.First(), states.First()));

        context.SaveChanges();
    }
}