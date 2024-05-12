using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateRepository : BaseRepositoryV2<State>
    {
        public StateRepository(DataContext context) : base(context)
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
