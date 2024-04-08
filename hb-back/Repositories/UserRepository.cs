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

        public async Task<User> GetUserByNickname(string nickname)
        {
            return await _context.Users.Where(u => u.Nickname == nickname).FirstOrDefaultAsync();
        }
    }
}
