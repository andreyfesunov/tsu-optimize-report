using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public class CommentRepository : ICommentRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<Comment> DbSet;

    public CommentRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<Comment>();
    }


    public async Task<Comment> AddEntity(Comment entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Comment> GetById(Guid id)
    {
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<Comment>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<Comment> UpdateEntity(Comment entity)
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

    public virtual async Task<Pagination<Comment>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(DbSet).Search(searchDto);
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    public IQueryable<Comment> IncludeChildren(IQueryable<Comment> query)
    {
        return query;
    }
}