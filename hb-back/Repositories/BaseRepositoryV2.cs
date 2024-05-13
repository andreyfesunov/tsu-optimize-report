using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories
{
    public abstract class BaseRepositoryV2<TEntity, DtoEntity> : IBaseRepository<TEntity, DtoEntity> where TEntity : Base
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbset;
        private readonly IMapper _mapper;

        public BaseRepositoryV2(DataContext context, IMapper mapper)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
            _mapper = mapper;
        }

        protected abstract IQueryable<TEntity> IncludeChildren(IQueryable<TEntity> query);

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

        public async Task<ICollection<DtoEntity>> GetAll()
        {
            var itemsQuery = _dbset.AsNoTracking().AsQueryable();

            return await IncludeChildren(itemsQuery).Select(x => _mapper.Map<DtoEntity>(x)).ToListAsync();
        }

        public async Task<DtoEntity> GetById(Guid id)
        {
            var entityQuery = _dbset.AsQueryable().Where(e => e.Id == id);
            var dtoEntitiy = (await IncludeChildren(entityQuery).Select(x => _mapper.Map<DtoEntity>(x)).ToListAsync())[0];
            return dtoEntitiy;
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

        public async Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto)
        {
            var count = await _dbset.CountAsync();
            var itemsQuery = _dbset.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize)
                .AsQueryable();

            itemsQuery = IncludeChildren(itemsQuery);

            return new PaginationDto<DtoEntity>
            {
                PageNumber = searchDto.PageNumber,
                PageSize = searchDto.PageSize,
                TotalPages = (count / searchDto.PageSize + count % searchDto.PageSize != 0 ? 1 : 0),
                Entities = await itemsQuery.Select(x => _mapper.Map<DtoEntity>(x)).ToListAsync()
            };
        }
    }
}
