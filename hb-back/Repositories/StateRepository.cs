using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class StateRepository : BaseRepositoryV2<State>
{
    private readonly DataContext _context;

    public StateRepository(
        DataContext context,
        UserRepository userRepository,
        StateUserRepository stateUserRepository
    ) : base(context)
    {
        _context = context;
    }

    protected override IQueryable<State> IncludeChildren(IQueryable<State> query)
    {
        return query
            .Include(x => x.Department)
            .ThenInclude(x => x.Institute)
            .Include(x => x.Job);
    }
}