using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain.Exceptions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Repositories;

public class StateRepository : IStateRepository
{
    private readonly DataContext _context;
    private readonly DbSet<State> _dbSet;

    public StateRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<State>();
    }


    public async Task<State> AddEntity(State entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<State> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<State>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<State> UpdateEntity(State entity)
    {
        var model = _context.Update(entity).Entity;
        await Save();
        return model;
    }

    public virtual async Task<Pagination<State>> Search(Search search)
    {
        return await IncludeChildren(_dbSet).Search(search);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        var entity = await GetById(entityId);
        if (entity == null) throw new AppException("Entity not found");
        _context.Remove(entity);
        return await Save();
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    // TODO to specification
    private static IQueryable<State> IncludeChildren(IQueryable<State> query)
    {
        return query
            .Include(x => x.Department)
            .ThenInclude(x => x.Institute)
            .Include(x => x.Job);
    }
}