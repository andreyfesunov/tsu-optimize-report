using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain.Exceptions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Repositories;

public class ActivityEventTypeRepository : IActivityEventTypeRepository
{
    private readonly DataContext _context;
    private readonly DbSet<ActivityEventType> _dbSet;

    public ActivityEventTypeRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<ActivityEventType>();
    }

    // TODO to specification
    public IQueryable<ActivityEventType> IncludeChildren(IQueryable<ActivityEventType> query)
    {
        return query;
    }

    public async Task<bool> Validate(ActivityEventType model)
    {
        var count = await _dbSet.Where(x => x.ActivityId == model.ActivityId && x.EventTypeId == model.EventTypeId)
            .CountAsync();
        return count == 0;
    }


    public async Task<ActivityEventType> AddEntity(ActivityEventType entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<ActivityEventType> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<ActivityEventType>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<ActivityEventType> UpdateEntity(ActivityEventType entity)
    {
        var model = _context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        var entity = await GetById(entityId);
        if (entity == null) throw new AppException("Entity not found");
        _context.Remove(entity);
        return await Save();
    }

    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<ICollection<ActivityEventType>> GetAll(Func<ActivityEventType, bool> predicate)
    {
        var queryWithIncludes = IncludeChildren(_dbSet.Where(predicate).AsQueryable());

        return queryWithIncludes.ToList();
    }

    public async Task<bool> DeleteBatch(IEnumerable<ActivityEventType> entities)
    {
        _context.RemoveRange(entities);
        return await Save();
    }

    public virtual async Task<Pagination<ActivityEventType>> Search(Search search)
    {
        return await IncludeChildren(_dbSet).Search(search);
    }
}