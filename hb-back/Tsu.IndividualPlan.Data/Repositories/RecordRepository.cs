using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Repositories;

public class RecordRepository : IRecordRepository
{
    private readonly DataContext _context;
    private readonly DbSet<Record> _dbSet;

    public RecordRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<Record>();
    }

    public async Task<Record[]> Get(Guid stateUserId, int? semestrId = null)
    {
        var result = IncludeChildren(_dbSet).Where(x => x.StateUserId == stateUserId);
        if (semestrId.HasValue)
            result = result.Where(x => x.SemestrId == semestrId);
        return await result.ToArrayAsync();
    }

    public IQueryable<Record> IncludeChildren(IQueryable<Record> query)
    {
        return query
            .Include(x => x.LessonType)
            .Include(x => x.Activity);
    }

    public async Task<Record> AddEntity(Record entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}