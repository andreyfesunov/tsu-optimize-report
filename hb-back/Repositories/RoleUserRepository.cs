using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class RoleUserRepository : BaseRepository<RoleUser>
    {
        private readonly DataContext _context;

        public RoleUserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
