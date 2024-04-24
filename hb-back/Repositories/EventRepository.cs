using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class EventRepository : BaseRepository<Event>
    {
        private readonly DataContext _context;

        public EventRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
