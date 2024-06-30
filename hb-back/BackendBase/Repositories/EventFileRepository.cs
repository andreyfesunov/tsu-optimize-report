using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class EventFileRepository : BaseRepository<EventFile>
    {
        public EventFileRepository(DataContext context) : base(context)
        {
        }
    }
}
