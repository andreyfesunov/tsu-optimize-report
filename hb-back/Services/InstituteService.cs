using BackendBase.Data;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services
{
    public class InstituteService //: CRUDServiceBase<Institute>, IInstituteService
    {
        public InstituteService(InstituteRepository repository)
        {
            //_repository = repository;
        }
    }
}
