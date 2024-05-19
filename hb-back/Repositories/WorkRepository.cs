using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories;

public class WorkRepository : BaseRepositoryV2<Work>
{
    public WorkRepository(DataContext context) : base(context)
    {
    }

    protected override IQueryable<Work> IncludeChildren(IQueryable<Work> query)
    {
        return query.OrderBy(x => x.Order);
    }
}