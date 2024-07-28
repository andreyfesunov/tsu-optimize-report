using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Extensions;
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

    public async Task<LessonType> GetById(Guid id)
    {
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<LessonType>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<LessonType> UpdateEntity(LessonType entity)
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

    public virtual async Task<Pagination<LessonType>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(DbSet).Search(searchDto);
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    public IQueryable<LessonType> IncludeChildren(IQueryable<LessonType> query)
    {
        return query;
    }

    public Task<LessonType?> GetLessonTypeByName(string name)
    {
        return Context.LessonTypes.Where(x => x.Name == name).FirstOrDefaultAsync();
    }

    public async Task<ICollection<LessonType>> GetAllForReport(Guid stateUserId)
    {
        return await DbSet.Where(x => x.Records.Count(x => x.StateUserId == stateUserId) != 0)
            .ToListAsync();
    }
}
