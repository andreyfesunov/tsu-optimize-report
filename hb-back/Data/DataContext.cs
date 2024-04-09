using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BackendBase.Data
{
    public class DataContext : DbContext
    {
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected readonly IConfiguration _configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}
