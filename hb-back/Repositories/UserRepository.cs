using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
