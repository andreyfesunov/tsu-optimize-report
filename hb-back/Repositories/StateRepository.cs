using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class StateRepository : BaseRepository<State>
{
    public StateRepository(
        DataContext context
    ) : base(context)
    {
    }

    protected override IQueryable<State> IncludeChildren(IQueryable<State> query)
    {
        return query
            .Include(x => x.Department)
            .ThenInclude(x => x.Institute)
            .Include(x => x.Job);
    }
}