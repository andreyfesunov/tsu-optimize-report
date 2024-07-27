using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class EventRepository : IEventRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<Event> DbSet;

    public EventRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<Event>();
    }

    public async Task<Event> AddEntity(Event entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Event> UpdateEntity(Event entity)
    {
        var model = Context.Update(entity).Entity;
        await Save();
        return model;
    }

    public async Task<Event> GetById(Guid id) { 
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    } 

    public async Task<bool> Delete(Event entity) {
        Context.Remove(entity);
        return await Save();
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    public IQueryable<Event> IncludeChildren(IQueryable<Event> query)
    {
        return query
            .Include(x => x.Lessons)
            .Include(x => x.Comments)
            .Include(x => x.EventType);
    }
}
