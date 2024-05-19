using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class ActivityEventTypeRepository : BaseRepositoryV2<ActivityEventType, ActivityEventTypeDto>
{
    public ActivityEventTypeRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    protected override IQueryable<ActivityEventType> IncludeChildren(IQueryable<ActivityEventType> query)
    {
        return query;
    }

    public async Task<bool> Validate(ActivityEventType model)
    {
        var count = await dbset.Where(x => x.ActivityId == model.ActivityId && x.EventTypeId == model.EventTypeId)
            .CountAsync();
        return count == 0;
    }
}