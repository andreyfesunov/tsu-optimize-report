using BackendBase.Data;
using BackendBase.Models;

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
