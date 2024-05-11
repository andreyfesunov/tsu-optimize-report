using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services
{
    public class DepartmentService : CRUDServiceBase<Department>, IDepartmentService
    {
        public DepartmentService(DepartmentRepository repository)
        {
            _repository = repository;
        }
    }
}
