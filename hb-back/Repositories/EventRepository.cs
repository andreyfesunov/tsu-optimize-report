using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class EventRepository : BaseRepository<Event>
    {
        public EventRepository(DataContext context) : base(context)
        {
        }
    }
}
