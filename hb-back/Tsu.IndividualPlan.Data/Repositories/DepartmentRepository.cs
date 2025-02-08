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

    // TODO to specification
    private static IQueryable<Department> IncludeChildren(IQueryable<Department> query)
    {
        return query
            .Include(x => x.Institute);
    }
}