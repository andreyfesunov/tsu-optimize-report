using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto.Report;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateUserRepository : BaseRepositoryV2<StateUser, ReportListDto>
    {
        public StateUserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<StateUser> IncludeChildren(IQueryable<StateUser> query)
        {
            return query.Include(x => x.Events)
                .Include(x => x.Files)
                .Include(x => x.Records)
                .Include(x => x.State)
                .ThenInclude(x => x.Job)
                .Include(x => x.State)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Institute)
                .Include(x => x.User);
        }
    }
}
