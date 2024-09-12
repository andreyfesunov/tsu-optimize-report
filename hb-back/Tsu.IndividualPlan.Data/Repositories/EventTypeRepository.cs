using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Repositories;

public class EventTypeRepository : IEventTypeRepository
{
    private readonly DataContext _context;
    private readonly DbSet<EventType> _dbSet;

    public EventTypeRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<EventType>();
    }

    public async Task<Pagination<EventType>> Search(Guid activityId, Search search)
    {
        var queryable = _context
            .Set<ActivityEventType>()
            .AsQueryable()
            .Where(x => x.ActivityId == activityId)
            .Select(x => x.EventType)
            .OfType<EventType>();

        return await queryable.Search(search);
    }

    public async Task<ICollection<EventType>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<ICollection<EventType>> GetAll(Func<EventType, bool> predicate)
    {
        var queryWithIncludes = IncludeChildren(_dbSet.Where(predicate).AsQueryable());

        return queryWithIncludes.ToList();
    }

    // TODO to specification
    private static IQueryable<EventType> IncludeChildren(IQueryable<EventType> query)
    {
        return query.Include(x => x.Work);
    }

    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}