using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateUserRepository : BaseRepositoryV2<StateUser>
    {
        public StateUserRepository(DataContext context) : base(context)
        {
        }

        protected override IQueryable<StateUser> IncludeChildren(IQueryable<StateUser> query)
        {
            return query.Include(x => x.Events)
                .Include(x => x.Files)
                .Include(x => x.Records)
                .Include(x => x.State)
                .ThenInclude(x => x.Job)
                .Include(x => x.State)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Institute)
                .Include(x => x.User);
        }
    }
}
