using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class ActivityEventTypeRepository : BaseRepositoryV2<ActivityEventType, ActivityEventTypeDto>
    {

        public ActivityEventTypeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        { }

        protected override IQueryable<ActivityEventType> IncludeChildren(IQueryable<ActivityEventType> query)
        {
            return query;
        }
    }
}
