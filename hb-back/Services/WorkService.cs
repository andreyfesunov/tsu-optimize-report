﻿using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class WorkService : IWorkService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<Work, WorkDto> _mappingHelper;
    protected IBaseRepository<Work> _repository;

    public WorkService(WorkRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Work, WorkDto>(_mapper);
    }

    public async Task<Work> AddEntity(Work entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<WorkDto> GetById(Guid id)
    {
        return _mappingHelper.toDto(await _repository.GetById(id));
    }

    public async Task<ICollection<WorkDto>> GetAll()
    {
        return _mappingHelper.toDto(await _repository.GetAll());
    }

    public async Task<Work> Update(Work entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<PaginationDto<WorkDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.paginationToDto(await _repository.Search(searchDto));
    }
}