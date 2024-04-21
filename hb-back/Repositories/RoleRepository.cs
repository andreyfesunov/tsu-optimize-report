﻿using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}