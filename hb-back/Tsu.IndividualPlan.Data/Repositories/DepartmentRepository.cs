using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Repositories;

public class DepartmentRepository(DataContext context) : IDepartmentRepository
{
    private readonly DbSet<Department> _dbSet = context.Set<Department>();

    public async Task<Pagination<Department>> Search(Search search)
    {
        return await IncludeChildren(_dbSet).Search(search);
    }

    public async Task<Department> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<Department>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await itemsQuery.ToListAsync();
    }

    // TODO to specification
    private static IQueryable<Department> IncludeChildren(IQueryable<Department> query)
    {
        return query
            .Include(x => x.Institute);
    }
}