using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Data;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using File = Tsu.IndividualPlan.WebApi.Models.File;

namespace Tsu.IndividualPlan.WebApi.Repositories;

public class FileRepository : IFileRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<File> DbSet;

    public FileRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<File>();
    }

    public async Task<File> AddEntity(File entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    private async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }
}