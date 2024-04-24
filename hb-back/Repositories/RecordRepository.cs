using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class RecordRepository : BaseRepository<Record>
    {
        private readonly DataContext _context;

        public RecordRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
