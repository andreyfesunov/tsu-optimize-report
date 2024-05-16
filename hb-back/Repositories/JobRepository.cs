using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class JobRepository : BaseRepositoryV2<Job, JobDto>
    {
        public JobRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Job> IncludeChildren(IQueryable<Job> query)
        {
            return query;
        }
    }
}
