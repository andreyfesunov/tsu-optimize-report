using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Extensions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class EventTypeRepository : IEventTypeRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<EventType> DbSet;

    public EventTypeRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<EventType>();
    }

    public IQueryable<EventType> IncludeChildren(IQueryable<EventType> query)
    {
        return query.Include(x => x.Work);
    }

    public async Task<Pagination<EventType>> Search(Guid activityId, SearchDto searchDto)
    {
        var queryable = Context
            .Set<ActivityEventType>()
            .AsQueryable()
            .Where(x => x.ActivityId == activityId)
            .Select(x => x.EventType)
            .OfType<EventType>();

        return await queryable.Search(searchDto);
    }

    public async Task<ICollection<EventType>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<ICollection<EventType>> GetAll(Func<EventType, bool> predicate)
    {
        var queryWithIncludes = IncludeChildren(DbSet.Where(predicate).AsQueryable());

        return queryWithIncludes.ToList();
    }
}
