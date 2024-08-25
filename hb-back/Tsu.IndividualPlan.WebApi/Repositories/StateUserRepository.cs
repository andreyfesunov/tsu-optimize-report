using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Data;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Repositories;

public class StateUserRepository : IStateUserRepository
{
    private readonly UserInfo _userInfo;
    protected readonly DataContext Context;
    protected readonly DbSet<StateUser> DbSet;

    public StateUserRepository(DataContext context, UserInfo userInfo)
    {
        Context = context;
        DbSet = Context.Set<StateUser>();
        _userInfo = userInfo;
    }

    public async Task<StateUser> GetById(Guid id)
    {
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<StateUser>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<StateUser> AddEntity(StateUser entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Pagination<StateUser>> Search(SearchDto dto)
    {
        return await IncludeChildren(DbSet)
            .Where(x => x.User != null && x.User.Id.ToString() == _userInfo.GetUserId())
            .Search(dto);
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    protected IQueryable<StateUser> IncludeChildren(IQueryable<StateUser> query)
    {
        return query
            .Include(x => x.Events)
            .ThenInclude(x => x.EventType)
            .ThenInclude(x => x.Work)
            .Include(x => x.Events)
            .ThenInclude(x => x.Comments)
            .Include(x => x.Events)
            .ThenInclude(x => x.Lessons)
            .ThenInclude(x => x.LessonType)
            .Include(x => x.User)
            .Include(x => x.Files)
            .Include(x => x.Records)
            .ThenInclude(x => x.Activity)
            .Include(x => x.State)
            .ThenInclude(x => x.Job)
            .Include(x => x.State)
            .ThenInclude(x => x.Department)
            .ThenInclude(x => x.Institute)
            .Include(x => x.Files);
    }
}