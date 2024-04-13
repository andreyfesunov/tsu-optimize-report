using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class FileRepository : BaseRepository<BackendBase.Models.File>
    {
        private readonly DataContext _context;

        public FileRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
