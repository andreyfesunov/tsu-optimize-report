using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class WorkRepository : BaseRepository<Work>
    {
        private readonly DataContext _context;

        public WorkRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
