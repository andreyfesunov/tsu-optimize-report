using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto.CreateDto;
using BackendBase.Dto.Report;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class StateUserRepository : BaseRepository<StateUser>
{
    public StateUserRepository(DataContext context) : base(context)
    {
    }

    //public async Task<bool> ExistStateUser(StateUserCreateDto stateUserCreateDto)
    //{
    //    return _context.Set<StateUser>().FirstOrDefault(x => x.StateId == Guid.Parse(stateUserCreateDto.StateId) && x.UserId == Guid.Parse(stateUserCreateDto.UserId)) == null;
    //}

    protected override IQueryable<StateUser> IncludeChildren(IQueryable<StateUser> query)
    {
        return query.Include(x => x.Events)
            .Include(x => x.User)
            .Include(x => x.Files)
            .Include(x => x.Records)
            .Include(x => x.State)
            .ThenInclude(x => x.Job)
            .Include(x => x.State)
            .ThenInclude(x => x.Department)
            .ThenInclude(x => x.Institute);
    }
}