using BackendBase.Models;
using BackendBase.Factories;
using Microsoft.EntityFrameworkCore;
using DBFile = BackendBase.Models.File;

namespace BackendBase.Data
{
    public class DataContext : DbContext
    {
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

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
            => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}
