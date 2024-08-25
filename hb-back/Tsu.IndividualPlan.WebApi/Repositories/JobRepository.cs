using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Data;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Repositories;

public class JobRepository : IJobRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<Job> DbSet;

    public JobRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<Job>();
    }

    public async Task<ICollection<Job>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await itemsQuery.ToListAsync();
    }
}