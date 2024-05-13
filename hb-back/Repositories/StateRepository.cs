using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateRepository : BaseRepositoryV2<State, StateDto>
    {
        public StateRepository(DataContext context, IMapper mapper) : base(context, mapper)
        { }

        protected override IQueryable<State> IncludeChildren(IQueryable<State> query)
        {
            return query
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Institute)
                    .Include(x => x.Job);
        }
    }
}
