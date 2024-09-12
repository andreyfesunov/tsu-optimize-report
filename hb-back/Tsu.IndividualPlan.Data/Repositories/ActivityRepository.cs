using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly DataContext _context;
    private readonly DbSet<Activity> _dbSet;

    public ActivityRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<Activity>();
    }

    public async Task<ICollection<Activity>> GetAll()
    {
        return await _dbSet.AsNoTracking().AsQueryable().ToListAsync();
    }

    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}