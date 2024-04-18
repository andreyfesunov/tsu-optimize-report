using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateUserRepository : BaseRepository<StateUser>
    {
        private readonly DataContext _context;

        public StateUserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
