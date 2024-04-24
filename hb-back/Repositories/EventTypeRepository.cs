﻿using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class EventTypeRepository : BaseRepository<EventType>
    {
        private readonly DataContext _context;

        public EventTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
