using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public class StateUserRepository : BaseRepository<StateUser>
{
    private readonly UserInfo _userInfo;

    public StateUserRepository(DataContext context, UserInfo userInfo) : base(context)
    {
        _userInfo = userInfo;
    }

    public override async Task<PaginationDto<StateUser>> Search(SearchDto searchDto)
    {
        return await IncludeChildren(dbset)
            .Where(x => x.User.Id.ToString() == _userInfo.GetUserId())
            .Search(searchDto);
    }

    protected override IQueryable<StateUser> IncludeChildren(IQueryable<StateUser> query)
    {
        return query
            .Include(x => x.Events)
            .ThenInclude(x => x.EventType)
            .ThenInclude(x => x.Work)
            .Include(x => x.Events)
            .ThenInclude(x => x.Comments)
            .Include(x => x.Events)
            .ThenInclude(x => x.Lessons)
            .ThenInclude(x => x.LessonType)
            .Include(x => x.User)
            .Include(x => x.Files)
            .Include(x => x.Records)
            .ThenInclude(x => x.Activity)
            .Include(x => x.State)
            .ThenInclude(x => x.Job)
            .Include(x => x.State)
            .ThenInclude(x => x.Department)
            .ThenInclude(x => x.Institute);
    }
}