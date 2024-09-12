using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Domain.Models.Business;
using DBFile = Tsu.IndividualPlan.Domain.Models.Business.File;

namespace Tsu.IndividualPlan.Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public required DbSet<Institute> Institutes { get; init; }
    public required DbSet<Job> Jobs { get; init; }
    public required DbSet<Department> Departments { get; init; }
    public required DbSet<State> States { get; init; }
    public required DbSet<User> Users { get; init; }
    public required DbSet<StateUser> StatesUsers { get; init; }
    public required DbSet<DBFile> Files { get; init; }
    public required DbSet<Lesson> Lessons { get; init; }
    public required DbSet<Activity> Activities { get; init; }
    public required DbSet<ActivityEventType> ActivitiesEventsTypes { get; init; }
    public required DbSet<Work> Work { get; init; }
    public required DbSet<EventType> EventsTypes { get; init; }
    public required DbSet<Event> Events { get; init; }
    public required DbSet<LessonType> LessonTypes { get; init; }
    public required DbSet<Record> Records { get; init; }
    public required DbSet<Rank> Ranks { get; init; }
    public required DbSet<Degree> Degrees { get; init; }
}