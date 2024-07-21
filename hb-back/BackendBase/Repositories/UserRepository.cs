﻿using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public class UserRepository : IUserRepository
{
    protected readonly DataContext Context;
    protected readonly DbSet<User> DbSet;

    public UserRepository(DataContext context)
    {
        Context = context;
        DbSet = Context.Set<User>();
    }


    public async Task<User> AddEntity(User entity)
    {
        var model = await DbSet.AddAsync(entity);
        await Save();
        return model.Entity;
    }

    public async Task<User> GetById(Guid id)
    {
        var entityQuery = DbSet.AsQueryable().Where(e => e.Id == id);
        return (await IncludeChildren(entityQuery).ToListAsync())[0];
    }

    public async Task<ICollection<User>> GetAll()
    {
        var itemsQuery = DbSet.AsNoTracking().AsQueryable();
        return await IncludeChildren(itemsQuery).ToListAsync();
    }

    public async Task<User> UpdateEntity(User entity)
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

    public async Task<Pagination<User>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(DbSet).Search(searchDto);
    }

    public async Task<bool> Save()
    {
        var saved = await Context.SaveChangesAsync();
        return saved > 0;
    }

    public IQueryable<User> IncludeChildren(IQueryable<User> query)
    {
        return query;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await Context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    }
}