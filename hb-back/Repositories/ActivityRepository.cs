using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class ActivityRepository : BaseRepositoryV2<Activity, ActivityDto>
    {
        public ActivityRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Activity> IncludeChildren(IQueryable<Activity> query)
        {
            return query;
        }
    }
}
