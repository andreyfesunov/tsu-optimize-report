using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Repositories;

public class StateUserRepository : IStateUserRepository
{
    private readonly DataContext _context;
    private readonly DbSet<StateUser> _dbSet;
    private readonly UserInfo _userInfo;

    public StateUserRepository(DataContext context, UserInfo userInfo)
    {
        _context = context;
        _dbSet = _context.Set<StateUser>();
        _userInfo = userInfo;
    }

    public async Task<StateUser> GetById(Guid id)
    {
        var entityQuery = _dbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<StateUser>> GetAll()
    {
        var itemsQuery = _dbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<IEnumerable<StateUser>> Get(IEnumerable<Guid> userIds)
    {
        return await _dbSet.AsQueryable()
            .Include(x => x.State)
            .Where(x => userIds.Contains(x.User.Id))
            .ToListAsync();
    }

    public async Task<StateUser> AddEntity(StateUser entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<Pagination<StateUser>> Search(Search search)
    {
        return await IncludeChildren(_dbSet)
            .Where(x => x.User != null && x.User.Id.ToString() == _userInfo.GetUserId())
            .Search(search);
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    private static IQueryable<StateUser> IncludeChildren(IQueryable<StateUser> query)
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