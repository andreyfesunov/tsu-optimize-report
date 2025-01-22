using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Repositories;

public class JobRepository : IJobRepository
{
    private readonly DataContext _context;
    private readonly DbSet<Job> _dbSet;

    public JobRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<Job>();
    }

    public async Task<ICollection<Job>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await itemsQuery.ToListAsync();
    }

    public async Task<Job> AddEntity(Job entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}