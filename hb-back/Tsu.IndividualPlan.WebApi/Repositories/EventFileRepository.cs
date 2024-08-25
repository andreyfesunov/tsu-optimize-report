using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Data;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Repositories;

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