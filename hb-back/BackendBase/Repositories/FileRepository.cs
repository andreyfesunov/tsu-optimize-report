using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using File = BackendBase.Models.File;

namespace BackendBase.Repositories
{
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

        private async Task<bool> Save() {
            var saved = await Context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
