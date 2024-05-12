using BackendBase.Data;
using BackendBase.Models;
using MathNet.Numerics.Statistics.Mcmc;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateUserRepository : BaseRepository<StateUser>
    {
        private readonly DataContext _context;
        private readonly DbSet<StateUser> _dbset;

        public StateUserRepository(DataContext context) : base(context)
        {
            _context = context;
            _dbset = _context.Set<StateUser>();
        }

        public async Task<StateUser> GetByIdInclude(Guid id)
        {
            return await _dbset
                .Include(x => x.Events)
                .Include(x => x.Files)
                .Include(x => x.Records)
                .Include(x => x.State)
                    .ThenInclude(x => x.Job)
                    .Include(x => x.State)
                    .ThenInclude(x => x.Department)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
