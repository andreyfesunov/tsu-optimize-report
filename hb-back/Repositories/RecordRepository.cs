using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class RecordRepository : BaseRepository<Record>
    {
        public RecordRepository(DataContext context) : base(context)
        {
        }
    }
}
