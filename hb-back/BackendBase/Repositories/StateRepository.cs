using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class StateRepository : BaseRepository<State>, IStateRepository
{
    public StateRepository(
        DataContext context
    ) : base(context)
    {
    }

    protected override IQueryable<State> IncludeChildren(IQueryable<State> query)
    {
        return query
            .Include(x => x.Department)
            .ThenInclude(x => x.Institute)
            .Include(x => x.Job);
    }
}
