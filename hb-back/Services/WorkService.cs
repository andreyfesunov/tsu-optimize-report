using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class WorkService : CRUDServiceBase<Work, WorkDto>, IWorkService
{
    public WorkService(WorkRepository repository)
    {
        _repository = repository;
    }
}