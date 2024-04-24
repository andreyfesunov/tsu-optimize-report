using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class JobRepository : BaseRepository<Job>
    {
        private readonly DataContext _context;

        public JobRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
