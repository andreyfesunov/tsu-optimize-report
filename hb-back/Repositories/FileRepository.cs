using BackendBase.Data;
using File = BackendBase.Models.File;

namespace BackendBase.Repositories
{
    public class FileRepository : BaseRepository<File>
    {
        private readonly DataContext _context;

        public FileRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
