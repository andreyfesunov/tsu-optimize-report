using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Comment> UpdateEntity(Comment entity)
    {
        var model = Context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<bool> Delete(Comment entity)
    {
        Context.Remove(entity);
        return await Save();
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
