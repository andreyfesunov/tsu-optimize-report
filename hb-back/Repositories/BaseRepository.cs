using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Base
{
    protected readonly DataContext context;
    protected readonly DbSet<TEntity> dbset;

    protected BaseRepository(DataContext context)
    {
        this.context = context;
        dbset = this.context.Set<TEntity>();
    }

    public async Task<TEntity> AddEntity(TEntity entity)
    {
        var model = await context.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<bool> Delete(TEntity entity)
    {
        context.Remove(entity);
        return await Save();
    }

    public async Task<bool> DeleteBatch(IEnumerable<TEntity> entities)
    {
        context.RemoveRange(entities);
        return await Save();
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        var entity = await GetById(entityId);
        if (entity == null) throw new EntityNotFoundException();
        context.Remove(entity);
        return await Save();
    }

    public async Task<bool> DoesExist(Guid id)
    {
        return await dbset.FindAsync(id) != null;
    }

    public async Task<ICollection<TEntity>> GetAll()
    {
        var itemsQuery = dbset.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        var entityQuery = dbset.AsQueryable().Where(e => e.Id == id);
        var dtoEntity = (await IncludeChildren(entityQuery).ToListAsync())[0];
        return dtoEntity;
    }

    public async Task<TEntity> GetByIdRoot(Guid id)
    {
        return await dbset.FindAsync(id);
    }

    public async Task<bool> Save()
    {
        var saved = await context.SaveChangesAsync();
        return saved > 0;
    }

    public ICollection<TEntity> SearchEntity(Func<TEntity, bool> predicate)
    {
        return dbset.Where(predicate).ToList();
    }

    public async Task<TEntity> UpdateEntity(TEntity entity)
    {
        var model = context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<PaginationDto<TEntity>> SearchRoot(SearchDto searchDto)
    {
        return await dbset.Search(searchDto);
    }

    public virtual async Task<PaginationDto<TEntity>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(dbset).Search(searchDto);
    }

    public async Task<ICollection<TEntity>> GetAll(Func<TEntity, bool> predicate)
    {
        var queryWithIncludes = IncludeChildren(dbset.Where(predicate).AsQueryable());

        return queryWithIncludes.ToList();
    }

    public async Task<ICollection<TEntity>> GetByIds(ICollection<Guid> ids)
    {
        var query = dbset.AsQueryable().Where(e => ids.Contains(e.Id));
        var entities = await IncludeChildren(query).ToListAsync();
        return entities;
    }

    protected virtual IQueryable<TEntity> IncludeChildren(IQueryable<TEntity> query)
    {
        return query;
    }
}