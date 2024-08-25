using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Models;
using DBFile = Tsu.IndividualPlan.WebApi.Models.File;

namespace Tsu.IndividualPlan.WebApi.Data;

public class DataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Institute> Institutes { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<StateUser> StatesUsers { get; set; }
    public DbSet<DBFile> Files { get; set; }
    public DbSet<EventFile> EventsFiles { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityEventType> ActivitiesEventsTypes { get; set; }
    public DbSet<Work> Work { get; set; }
    public DbSet<EventType> EventsTypes { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<LessonType> LessonTypes { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<Rank> Ranks { get; set; }
    public DbSet<Degree> Degrees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}