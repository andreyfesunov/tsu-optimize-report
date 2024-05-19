using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories;

public class EventTypeRepository : BaseRepositoryV2<EventType, EventTypeDto>
{
    public EventTypeRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    protected override IQueryable<EventType> IncludeChildren(IQueryable<EventType> query)
    {
        return query;
    }

    public Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto)
    {
        var queryable = context.Set<ActivityEventType>().AsQueryable().Where(x => x.ActivityId == activityId)
            .Select(x => x.EventType);

        return SearchFunc(queryable, searchDto);
    }
}