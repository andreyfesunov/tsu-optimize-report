using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class RecordService : IRecordService
{
    private readonly IMapper _mapper;
    private readonly RecordRepository _recordRepository;

    public RecordService(RecordRepository recordRepository, IMapper mapper)
    {
        _recordRepository = recordRepository;
        _mapper = mapper;
    }

    public async Task<RecordDto[]> Get(Guid stateUserId)
    {
        var records = await _recordRepository.Get(stateUserId);
        return records.Select(x => _mapper.Map<RecordDto>(x)).ToArray();
    }
}