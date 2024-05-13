using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class DepartmentRepository : BaseRepositoryV2<Department, DepartmentDto>
    {
        public DepartmentRepository(DataContext context, IMapper mapper) : base(context, mapper)
        { }

        protected override IQueryable<Department> IncludeChildren(IQueryable<Department> query)
        {
            return query
                    .Include(x => x.Institute);
        }
    }
}
