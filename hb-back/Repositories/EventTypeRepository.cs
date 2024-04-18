using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class EventTypeRepository : BaseRepository<EventType>
    {
        private readonly DataContext _context;

        public EventTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
