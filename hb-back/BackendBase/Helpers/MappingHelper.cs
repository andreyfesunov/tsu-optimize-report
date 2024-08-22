using AutoMapper;

namespace BackendBase.Helpers;

[Obsolete]
public class MappingHelper<TEntity, TEntityDto>
{
    private readonly IMapper _mapper;

    public MappingHelper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TEntityDto ToDto(TEntity entity)
    {
        return _mapper.Map<TEntityDto>(entity);
    }

    public ICollection<TEntityDto> ToDto(IEnumerable<TEntity> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}
