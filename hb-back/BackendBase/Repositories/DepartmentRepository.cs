using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataContext context) : base(context)
        { }

        protected override IQueryable<Department> IncludeChildren(IQueryable<Department> query)
        {
            return query
                    .Include(x => x.Institute);
        }
    }
}
