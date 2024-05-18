using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories;

public class WorkRepository : BaseRepositoryV2<Work, WorkDto>
{
    public WorkRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    protected override IQueryable<Work> IncludeChildren(IQueryable<Work> query)
    {
        return query;
    }
}