using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class ActivityService : CRUDServiceBase<Activity, ActivityDto>, IActivityService
{
    public ActivityService(ActivityRepository repository)
    {
        _repository = repository;
    }
}