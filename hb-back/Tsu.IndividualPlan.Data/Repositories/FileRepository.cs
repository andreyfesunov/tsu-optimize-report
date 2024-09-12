using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using File = Tsu.IndividualPlan.Domain.Models.Business.File;

namespace Tsu.IndividualPlan.Data.Repositories;

public class FileRepository : IFileRepository
{
    private readonly DataContext _context;
    private readonly DbSet<File> _dbSet;

    public FileRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<File>();
    }

    public async Task<File> AddEntity(File entity)
    {
        var model = await _dbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}