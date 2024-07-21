using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        protected readonly DataContext Context;
        protected readonly DbSet<Department> DbSet;

        public DepartmentRepository(DataContext context)
        {
            Context = context;
            DbSet = Context.Set<Department>();
        }

        public IQueryable<Department> IncludeChildren(IQueryable<Department> query)
        {
            return query
                    .Include(x => x.Institute);
        }

        public async Task<Pagination<Department>> Search(SearchDto searchDto)
        {
            return await IncludeChildren(DbSet).Search(searchDto);
        }
    }
}
