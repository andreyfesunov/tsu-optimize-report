using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class InstituteRepository : BaseRepositoryV2<Institute, InstituteDto>
    {
        public InstituteRepository(DataContext context, IMapper mapper) : base(context, mapper)
        { }

        protected override IQueryable<Institute> IncludeChildren(IQueryable<Institute> query)
        {
            return query;
        }
    }
}
