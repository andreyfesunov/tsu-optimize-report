﻿using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Services;

public class JobService : IJobService
{
    protected IJobRepository _repository;

    public JobService(IJobRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Job>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Job> AddEntity(string name)
    {
        var entity = new Job(
            Name: name
        );

        return await _repository.AddEntity(entity);
    }
}