using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class InstituteRepository : BaseRepository<Institute>
    {
        private readonly DataContext _context;

        public InstituteRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
