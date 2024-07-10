﻿using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class ActivityEventTypeRepository : BaseRepository<ActivityEventType>
{
    public ActivityEventTypeRepository(DataContext context) : base(context)
    {
    }

    protected override IQueryable<ActivityEventType> IncludeChildren(IQueryable<ActivityEventType> query)
    {
        return query;
    }

    public async Task<bool> Validate(ActivityEventType model)
    {
        var count = await DbSet.Where(x => x.ActivityId == model.ActivityId && x.EventTypeId == model.EventTypeId)
            .CountAsync();
        return count == 0;
    }
}