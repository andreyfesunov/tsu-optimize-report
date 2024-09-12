using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Repositories;

public class JobRepository(DataContext context) : IJobRepository
{
    private readonly DbSet<Job> _dbSet = context.Set<Job>();

    public async Task<ICollection<Job>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await itemsQuery.ToListAsync();
    }
}