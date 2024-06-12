using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class EventRepository : BaseRepository<Event>
{
    public EventRepository(DataContext context) : base(context)
    {
    }

    protected override IQueryable<Event> IncludeChildren(IQueryable<Event> query)
    {
        return query.Include(x => x.EventType);
    }
}