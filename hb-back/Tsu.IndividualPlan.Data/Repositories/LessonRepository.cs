using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly DataContext _context;
    private readonly DbSet<Lesson> _dbSet;

    public LessonRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<Lesson>();
    }

    public async Task<Lesson> AddEntity(Lesson entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Lesson> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<Lesson> UpdateEntity(Lesson entity)
    {
        var model = _context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<bool> Delete(Lesson entity)
    {
        _context.Remove(entity);
        return await Save();
    }

    public async Task<ICollection<Lesson>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<Pagination<Lesson>> Search(Search search)
    {
        return await IncludeChildren(_dbSet).Search(search);
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    private static IQueryable<Lesson> IncludeChildren(IQueryable<Lesson> query)
    {
        return query;
    }
}