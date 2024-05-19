using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class DepartmentRepository : BaseRepositoryV2<Department>
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
