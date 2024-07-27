using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Extensions;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        protected readonly DataContext Context;
        protected readonly DbSet<Lesson> DbSet;

        public LessonRepository(DataContext context)
        {
            Context = context;
            DbSet = Context.Set<Lesson>();
        }


        public async Task<Lesson> AddEntity(Lesson entity)
        {
            var model = await DbSet.AddAsync(entity);
            await Save();
            return model.Entity;
        }

        public async Task<Lesson> GetById(Guid id)
        {
            var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
            return (await IncludeChildren(entityQuery).ToListAsync())[0];
        }

        public async Task<ICollection<Lesson>> GetAll()
        {
            var itemsQuery = DbSet.AsNoTracking().AsQueryable();
            return await IncludeChildren(itemsQuery).ToListAsync();
        }

        public async Task<Lesson> UpdateEntity(Lesson entity)
        {
            var model = Context.Update(entity).Entity;
            await Save();
            return model;
        }

        public async Task<bool> DeleteById(Guid entityId)
        {
            var entity = await GetById(entityId);
            if (entity == null) throw new AppException("Entity not found");
            Context.Remove(entity);
            return await Save();
        }

        public async Task<Pagination<Lesson>> Search(SearchDto searchDto)
        {
            return await IncludeChildren(DbSet).Search(searchDto);
        }

        public async Task<bool> Save()
        {
            var saved = await Context.SaveChangesAsync();
            return saved > 0;
        }

        public IQueryable<Lesson> IncludeChildren(IQueryable<Lesson> query)
        {
            return query;
        }
    }
}
