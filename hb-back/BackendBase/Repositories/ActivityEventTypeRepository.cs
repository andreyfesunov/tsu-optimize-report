using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using BackendBase.Exceptions;

namespace BackendBase.Repositories;

public class ActivityEventTypeRepository : IActivityEventTypeRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<ActivityEventType> DbSet;

    public ActivityEventTypeRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<ActivityEventType>();
    }

    public IQueryable<ActivityEventType> IncludeChildren(IQueryable<ActivityEventType> query)
    {
        return query;
    }

    public async Task<bool> Validate(ActivityEventType model)
    {
        var count = await DbSet.Where(x => x.ActivityId == model.ActivityId && x.EventTypeId == model.EventTypeId)
            .CountAsync();
        return count == 0;
    }


    public async Task<ActivityEventType> AddEntity(ActivityEventType entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<ActivityEventType> GetById(Guid id)
    {
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<ActivityEventType>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<ActivityEventType> UpdateEntity(ActivityEventType entity)
    {
        var model = Context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        var entity = await GetById(entityId);
        if (entity == null) throw new AppException("Entity not found");
        Context.Remove(entity);
        return await Save();
    }

    public virtual async Task<Pagination<ActivityEventType>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(DbSet).Search(searchDto);
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<ICollection<ActivityEventType>> GetAll(Func<ActivityEventType, bool> predicate)
    {
        var queryWithIncludes = IncludeChildren(DbSet.Where(predicate).AsQueryable());

        return queryWithIncludes.ToList();
    }

    public async Task<bool> DeleteBatch(IEnumerable<ActivityEventType> entities)
    {
        Context.RemoveRange(entities);
        return await Save();
    }
}
