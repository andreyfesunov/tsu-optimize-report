using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class ActivtyRepository : BaseRepository<Activity>
    {
        private readonly DataContext _context;

        public ActivtyRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
