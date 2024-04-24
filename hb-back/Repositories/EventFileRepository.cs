using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class EventFileRepository : BaseRepository<EventFile>
    {
        private readonly DataContext _context;

        public EventFileRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
