using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class ActivityEventTypeRepository : BaseRepository<ActivityEventType>
    {
        private readonly DataContext _context;

        public ActivityEventTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
