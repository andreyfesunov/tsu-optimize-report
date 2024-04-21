using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class ActivityRepository : BaseRepository<Activity>
    {
        private readonly DataContext _context;

        public ActivityRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
