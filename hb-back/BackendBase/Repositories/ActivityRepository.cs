using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        protected readonly DataContext Context;
        protected readonly DbSet<Activity> DbSet;

        public ActivityRepository(DataContext context)
        {
            Context = context;
            DbSet = Context.Set<Activity>();
        }

        public async Task<ICollection<Activity>> GetAll()
        {
            return await DbSet.AsNoTracking().AsQueryable().ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await Context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
