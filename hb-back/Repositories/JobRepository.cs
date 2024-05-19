using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class JobRepository : BaseRepositoryV2<Job>
    {
        public JobRepository(DataContext context) : base(context)
        {
        }

        protected override IQueryable<Job> IncludeChildren(IQueryable<Job> query)
        {
            return query;
        }
    }
}
