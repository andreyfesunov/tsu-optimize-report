using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class StateRepository : BaseRepository<State>
    {
        private readonly DataContext _context;

        public StateRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
