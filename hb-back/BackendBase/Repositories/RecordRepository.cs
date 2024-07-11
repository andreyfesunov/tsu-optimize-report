using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class RecordRepository : IRecordRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<Record> DbSet;

    public RecordRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<Record>();
    }

    public async Task<Record[]> Get(Guid stateUserId)
    {
        return await IncludeChildren(DbSet).Where(x => x.StateUserId == stateUserId).ToArrayAsync();
    }

    public IQueryable<Record> IncludeChildren(IQueryable<Record> query)
    {
        return query
            .Include(x => x.LessonType)
            .Include(x => x.Activity);
    }

    public async Task<Record> AddEntity(Record entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }
}