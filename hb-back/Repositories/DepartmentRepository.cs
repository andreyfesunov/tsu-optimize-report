using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
