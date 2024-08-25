using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Data;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<Department> DbSet;

    public DepartmentRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<Department>();
    }

    public async Task<Pagination<Department>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(DbSet).Search(searchDto);
    }

    public IQueryable<Department> IncludeChildren(IQueryable<Department> query)
    {
        return query
            .Include(x => x.Institute);
    }
}