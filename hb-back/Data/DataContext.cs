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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}
