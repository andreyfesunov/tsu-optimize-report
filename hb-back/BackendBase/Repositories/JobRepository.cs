using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

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
