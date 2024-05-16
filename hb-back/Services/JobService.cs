using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class JobService : CRUDServiceBase<Job, JobDto>, IJobService
{
    public JobService(JobRepository repository)
    {
        _repository = repository;
    }
}