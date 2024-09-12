using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Repositories;

public class EventRepository : IEventRepository
{
    private readonly DataContext _context;
    private readonly DbSet<Event> _dbSet;

    public EventRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<Event>();
    }

    public async Task<Event> AddEntity(Event entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Event> UpdateEntity(Event entity)
    {
        var model = _context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<Event> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<bool> Delete(Event entity)
    {
        _context.Remove(entity);
        return await Save();
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    private static IQueryable<Event> IncludeChildren(IQueryable<Event> query)
    {
        return query
            .Include(x => x.Lessons)
            .Include(x => x.Comments)
            .Include(x => x.EventType);
    }
}