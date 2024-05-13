using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity, TEntity> where TEntity : Base
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

        public async Task<bool> DeleteById(Guid entityId)
        {
            var entity = await GetById(entityId);
            if (entity == null) throw new EntityNotFoundException();
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> DoesExist(Guid id)
        {
            return await _dbset.FindAsync(id) != null;
        }

        public async Task<ICollection<TEntity>> GetAll()
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

        public ICollection<TEntity> SearchEntity(Func<TEntity, bool> predicate)
        {
            return _dbset.Where(predicate).ToList();
        }

        public async Task<TEntity> UpdateEntity(TEntity entity)
        {
            var model = _context.Update(entity).Entity;
            await Save();
            return model;
        }

        public async Task<PaginationDto<TEntity>> Search(SearchDto searchDto)
        {
            var count = await _dbset.CountAsync();
            var items = await _dbset.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize)
                .ToListAsync();

            return new PaginationDto<TEntity>
            {
                PageNumber = searchDto.PageNumber,
                PageSize = searchDto.PageSize,
                TotalPages = (count / searchDto.PageSize + count % searchDto.PageSize != 0 ? 1 : 0),
                Entities = items
            };
        }
    }
}
