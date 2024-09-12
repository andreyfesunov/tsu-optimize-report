using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DataContext _context;
    private readonly DbSet<Comment> _dbSet;

    public CommentRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<Comment>();
    }


    public async Task<Comment> AddEntity(Comment entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Comment> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<Comment> UpdateEntity(Comment entity)
    {
        var model = _context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<bool> Delete(Comment entity)
    {
        _context.Remove(entity);
        return await Save();
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    // TODO to specification
    private static IQueryable<Comment> IncludeChildren(IQueryable<Comment> query)
    {
        return query;
    }
}