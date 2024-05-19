using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public abstract class BaseRepositoryV2<TEntity, DtoEntity> : IBaseRepository<TEntity, DtoEntity> where TEntity : Base
{
    protected readonly DataContext context;
    protected readonly DbSet<TEntity> dbset;
    protected readonly IMapper mapper;

    protected BaseRepositoryV2(DataContext context, IMapper mapper)
    {
        this.context = context;
        dbset = this.context.Set<TEntity>();
        this.mapper = mapper;
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

    public async Task<ICollection<DtoEntity>> GetAll()
    {
        var itemsQuery = dbset.AsNoTracking().AsQueryable();

        return await IncludeChildren(itemsQuery).Select(x => mapper.Map<DtoEntity>(x)).ToListAsync();
    }

    public async Task<DtoEntity> GetById(Guid id)
    {
        var entityQuery = dbset.AsQueryable().Where(e => e.Id == id);
        var dtoEntity = (await IncludeChildren(entityQuery).Select(x => mapper.Map<DtoEntity>(x)).ToListAsync())[0];
        return dtoEntity;
    }

    public async Task<TEntity> GetEntityById(Guid id)
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

    public async Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto)
    {
        return await SearchFunc(dbset, searchDto);
    }

    protected async Task<PaginationDto<DtoEntity>> SearchFunc(IQueryable<TEntity> queryable, SearchDto searchDto)
    {
        var count = await queryable.CountAsync();
        var itemsQuery = queryable.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize)
            .AsQueryable();

        itemsQuery = IncludeChildren(itemsQuery);

        return new PaginationDto<DtoEntity>
        {
            PageNumber = searchDto.PageNumber,
            PageSize = searchDto.PageSize,
            TotalPages = count / searchDto.PageSize + count % searchDto.PageSize != 0 ? 1 : 0,
            Entities = await itemsQuery.Select(x => mapper.Map<DtoEntity>(x)).ToListAsync()
        };
    }

    protected abstract IQueryable<TEntity> IncludeChildren(IQueryable<TEntity> query);
}