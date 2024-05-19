using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class InstituteRepository : BaseRepositoryV2<Institute>
    {
        public InstituteRepository(DataContext context) : base(context)
        { }

        protected override IQueryable<Institute> IncludeChildren(IQueryable<Institute> query)
        {
            return query;
        }
    }
}
