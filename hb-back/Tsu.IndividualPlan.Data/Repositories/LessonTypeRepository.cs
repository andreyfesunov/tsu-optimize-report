using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Repositories;

public class LessonTypeRepository : ILessonTypeRepository
{
    private readonly DataContext _context;
    private readonly DbSet<LessonType> _dbSet;

    public LessonTypeRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<LessonType>();
    }

    public async Task<LessonType> AddEntity(LessonType entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public Task<LessonType?> GetLessonTypeByName(string name)
    {
        return _context.LessonTypes.Where(x => x.Name == name).FirstOrDefaultAsync();
    }

    public async Task<ICollection<LessonType>> GetAllForReport(Guid stateUserId)
    {
        return await _dbSet
            .Where(x =>
                x.Records != null && x.Records.Count(x => x.StateUserId == stateUserId) != 0
            )
            .ToListAsync();
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}