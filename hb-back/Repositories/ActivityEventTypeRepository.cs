using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

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
