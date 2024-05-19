using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class UserRepository : BaseRepositoryV2<User>
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

        protected override IQueryable<User> IncludeChildren(IQueryable<User> query)
        {
            return query;
        }
    }
}
