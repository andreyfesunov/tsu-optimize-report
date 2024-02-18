using BackendBase.Data;
using BackendBase.Models;
using MongoDB.Driver;

namespace BackendBase.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private MongoDataContext _context;
        private IMongoCollection<User> _collection;

        public UserRepository(MongoDataContext context) : base(context, "users")
        {
            _context = context;
            _collection = _context.GetCollection<User>("users");
        }

        public async Task<User> GetByNickname(string nickName)
        {
            return await _collection.FindAsync(x => x.Nickname == nickName).Result.FirstOrDefaultAsync();
        }
    }
}
