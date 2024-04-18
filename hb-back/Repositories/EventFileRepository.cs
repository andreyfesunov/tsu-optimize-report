using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

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
