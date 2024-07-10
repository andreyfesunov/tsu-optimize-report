using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class StateService : IStateService
{
    private readonly DepartmentRepository _departmentRepository;
    protected readonly IMapper _mapper;
    private readonly StateRepository _stateRepository;
    private readonly StateUserRepository _stateUserRepository;
    private readonly UserRepository _userRepository;
    protected MappingHelper<State, StateDto> _mappingHelper;
    protected StateRepository _repository;

    public StateService(
        StateRepository repository,
        DepartmentRepository departmentRepository,
        UserRepository userRepository,
        StateUserRepository stateUserRepository,
        IMapper mapper
    )
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
        _stateRepository = repository;
        _userRepository = userRepository;
        _stateUserRepository = stateUserRepository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<State, StateDto>(_mapper);
    }

    public async Task<State> AddEntity(State entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<StateDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<StateDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
    }

    public async Task<State> Update(State entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<StateDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
    }

    public async Task<State> AddStateWithCreateDto(StateCreateDto stateCreateDto)
    {
        var state = _mapper.Map<State>(stateCreateDto);
        var test = await _departmentRepository.Search(new SearchDto { PageNumber = 1, PageSize = 1 });
        state.DepartmentId = test.Entities.First().Id;
        var model = await _stateRepository.AddEntity(state);
        return model;
    }

    public async Task<bool> Assign(StateUserCreateDto stateUserCreateDto)
    {
        var state = await _stateRepository.GetByIdRoot(Guid.Parse(stateUserCreateDto.StateId));
        var userDto = await _userRepository.GetById(Guid.Parse(stateUserCreateDto.UserId));
        if (state == null || userDto == null)
            return false;
        if (state.Count < 1)
            return false;

        state.Count -= 1;
        await _stateRepository.UpdateEntity(state);

        var stateUser = _mapper.Map<StateUser>(stateUserCreateDto);
        stateUser.Rate = 1.0;

        await _stateUserRepository.AddEntity(stateUser);

        return true;
    }
}