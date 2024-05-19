using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using MathNet.Numerics.Statistics.Mcmc;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class EventTypeRepository : BaseRepositoryV2<EventType>
{
    public EventTypeRepository(DataContext context) : base(context)
    {
    }

    protected override IQueryable<EventType> IncludeChildren(IQueryable<EventType> query)
    {
        return query;
    }

    public async Task<PaginationDto<EventType>> Search(Guid activityId, SearchDto searchDto)
    {
        var queryable = context.Set<ActivityEventType>().AsQueryable().Where(x => x.ActivityId == activityId)
            .Select(x => x.EventType);

        return await SearchFunc(queryable, searchDto);
    }

    protected async Task<PaginationDto<EventType>> SearchFunc(IQueryable<EventType> queryable, SearchDto searchDto)
    {
        var count = await queryable.CountAsync();
        var itemsQuery = queryable.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize)
            .AsQueryable();

        itemsQuery = IncludeChildren(itemsQuery);

        return new PaginationDto<EventType>
        {
            PageNumber = searchDto.PageNumber,
            PageSize = searchDto.PageSize,
            TotalPages = count / searchDto.PageSize + count % searchDto.PageSize != 0 ? 1 : 0,
            Entities = (ICollection<EventType>)itemsQuery
        };
    }

}