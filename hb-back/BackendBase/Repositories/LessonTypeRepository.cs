using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class LessonTypeRepository : BaseRepository<LessonType>
{
    public LessonTypeRepository(DataContext context) : base(context)
    {
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