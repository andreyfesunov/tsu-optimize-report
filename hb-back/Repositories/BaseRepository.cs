using BackendBase.Data;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;


namespace BackendBase.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbset;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }

        public async Task<TEntity> AddEntity(TEntity entity)
        {
            var model = await _context.AddAsync(entity);
            await Save();
            return model.Entity;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> DoesExist(Guid id)
        {
            return await _dbset.FindAsync(id) != null;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbset.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public List<TEntity> SearchEntity(Func<TEntity, bool> predicate)
        {
            return _dbset.Where(predicate).ToList();
        }

        public async Task<TEntity> UpdateEntity(TEntity entity)
        {
            var model = _context.Update(entity).Entity;
            await Save();
            return model;
        }
    }
}
