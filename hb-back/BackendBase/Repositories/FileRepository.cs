using BackendBase.Data;
using BackendBase.Models;
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
    }
}
