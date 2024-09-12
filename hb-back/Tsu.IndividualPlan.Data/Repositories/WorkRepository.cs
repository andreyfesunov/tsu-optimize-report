using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain.Exceptions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Repositories;

public class WorkRepository : IWorkRepository
{
    private readonly DataContext _context;
    private readonly DbSet<Work> _dbSet;

    public WorkRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<Work>();
    }


    public async Task<Work> AddEntity(Work entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Work> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<Work>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<Work> UpdateEntity(Work entity)
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

    public virtual async Task<Pagination<Work>> Search(Search search)
    {
        return await IncludeChildren(_dbSet).Search(search);
    }

    public IQueryable<Work> IncludeChildren(IQueryable<Work> query)
    {
        return query.OrderBy(x => x.Order);
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}