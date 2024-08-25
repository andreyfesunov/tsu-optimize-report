using System.Diagnostics;
using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class LessonTypeRepository : ILessonTypeRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<LessonType> DbSet;

    public LessonTypeRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<LessonType>();
    }

    public async Task<LessonType> AddEntity(LessonType entity)
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

    public Task<LessonType?> GetLessonTypeByName(string name)
    {
        return Context.LessonTypes.Where(x => x.Name == name).FirstOrDefaultAsync();
    }

    public async Task<ICollection<LessonType>> GetAllForReport(Guid stateUserId)
    {
        return await DbSet
            .Where(x =>
                x.Records != null && x.Records.Count(x => x.StateUserId == stateUserId) != 0
            )
            .ToListAsync();
    }
}
