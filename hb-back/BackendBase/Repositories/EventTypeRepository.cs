using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class EventTypeRepository : BaseRepository<EventType>
{
    public EventTypeRepository(DataContext context) : base(context)
    {
    }

    protected override IQueryable<EventType> IncludeChildren(IQueryable<EventType> query)
    {
        return query.Include(x => x.Work);
    }

    public async Task<PaginationDto<EventType>> Search(Guid activityId, SearchDto searchDto)
    {
        var queryable = context.Set<ActivityEventType>().AsQueryable().Where(x => x.ActivityId == activityId)
            .Select(x => x.EventType);

        return await queryable.Search(searchDto);
    }
}