using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class RecordRepository : BaseRepository<Record>
{
    public RecordRepository(DataContext context) : base(context)
    {
    }

    public async Task<Record[]> Get(Guid stateUserId)
    {
        return await IncludeChildren(dbset).Where(x => x.StateUserId == stateUserId).ToArrayAsync();
    }

    protected override IQueryable<Record> IncludeChildren(IQueryable<Record> query)
    {
        return query
            .Include(x => x.LessonType)
            .Include(x => x.Activity);
    }
}