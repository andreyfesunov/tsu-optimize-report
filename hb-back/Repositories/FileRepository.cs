using BackendBase.Data;
using File = BackendBase.Models.File;

namespace BackendBase.Repositories
{
    public class FileRepository : BaseRepository<File>
    {
        public FileRepository(DataContext context) : base(context)
        {
        }
    }
}
