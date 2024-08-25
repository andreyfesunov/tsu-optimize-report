using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Data;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Exceptions;
using Tsu.IndividualPlan.WebApi.Extensions;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Repositories;

public class WorkRepository : IWorkRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<Work> DbSet;

    public WorkRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<Work>();
    }


    public async Task<Work> AddEntity(Work entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Work> GetById(Guid id)
    {
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<Work>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<Work> UpdateEntity(Work entity)
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

    public virtual async Task<Pagination<Work>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(DbSet).Search(searchDto);
    }

    public IQueryable<Work> IncludeChildren(IQueryable<Work> query)
    {
        return query.OrderBy(x => x.Order);
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }
}