using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services
{
    public class InstituteService : CRUDServiceBase<Institute, InstituteDto>, IInstituteService
    {
        public InstituteService(InstituteRepository repository, IMapper mapper) : base(mapper)
        {
            _repository = repository;
        }
    }
}
