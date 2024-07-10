using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Base
{
    protected readonly DataContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public async Task<TEntity> AddEntity(TEntity entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<bool> Delete(TEntity entity)
    {
        Context.Remove(entity);
        return await Save();
    }

    public async Task<bool> DeleteBatch(IEnumerable<TEntity> entities)
    {
        Context.RemoveRange(entities);
        return await Save();
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        var entity = await GetById(entityId);
        if (entity == null) throw new AppException("Entity not found");
        Context.Remove(entity);
        return await Save();
    }

    public async Task<bool> DoesExist(Guid id)
    {
        return await DbSet.FindAsync(id) != null;
    }

    public async Task<ICollection<TEntity>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<TEntity> GetByIdRoot(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    public ICollection<TEntity> SearchEntity(Func<TEntity, bool> predicate)
    {
        return DbSet.Where(predicate).ToList();
    }

    public async Task<TEntity> UpdateEntity(TEntity entity)
    {
        var model = Context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<Pagination<TEntity>> SearchRoot(SearchDto searchDto)
    {
        return await DbSet.Search(searchDto);
    }

    public virtual async Task<Pagination<TEntity>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(DbSet).Search(searchDto);
    }

    public async Task<ICollection<TEntity>> GetAll(Func<TEntity, bool> predicate)
    {
        var queryWithIncludes = IncludeChildren(DbSet.Where(predicate).AsQueryable());

        return queryWithIncludes.ToList();
    }

    protected virtual IQueryable<TEntity> IncludeChildren(IQueryable<TEntity> query)
    {
        return query;
    }
}