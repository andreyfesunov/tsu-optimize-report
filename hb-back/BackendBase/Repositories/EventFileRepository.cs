using BackendBase.Data;
using BackendBase.Interfaces.Repositories;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class EventFileRepository : IEventFileRepository
    {
        protected readonly DataContext Context;
        protected readonly DbSet<EventFile> DbSet;

        public EventFileRepository(DataContext context)
        {
            Context = context;
            DbSet = Context.Set<EventFile>();
        }
    }
}
