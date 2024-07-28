using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Extensions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class JobRepository : IJobRepository
    {
        protected readonly DataContext Context;
        protected readonly DbSet<Job> DbSet;

        public JobRepository(DataContext context)
        {
            Context = context;
            DbSet = Context.Set<Job>();
        }

        public IQueryable<Job> IncludeChildren(IQueryable<Job> query)
        {
            return query;
        }


        public async Task<Job> AddEntity(Job entity)
        {
            var model = await DbSet.AddAsync(entity);
            await Save();
            return model.Entity;
        }

        public async Task<Job> GetById(Guid id)
        {
            var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
            return (await IncludeChildren(entityQuery).ToListAsync())[0];
        }

        public async Task<ICollection<Job>> GetAll()
        {
            var itemsQuery = DbSet.AsNoTracking().AsQueryable();
            return await IncludeChildren(itemsQuery).ToListAsync();
        }

        public async Task<Job> UpdateEntity(Job entity)
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

        public virtual async Task<Pagination<Job>> Search(SearchDto searchDto)
        {
            return await IncludeChildren(DbSet).Search(searchDto);
        }

        public async Task<bool> Save()
        {
            var saved = await Context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
