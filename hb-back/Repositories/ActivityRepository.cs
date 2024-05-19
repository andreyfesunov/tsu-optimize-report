using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class ActivityRepository : BaseRepositoryV2<Activity>
    {
        public ActivityRepository(DataContext context) : base(context)
        {
        }

        protected override IQueryable<Activity> IncludeChildren(IQueryable<Activity> query)
        {
            return query;
        }
    }
}
