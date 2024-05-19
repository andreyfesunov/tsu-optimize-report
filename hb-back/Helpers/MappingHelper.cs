using AutoMapper;
using BackendBase.Models;

namespace BackendBase.Helpers
{
    public static class MappingHelper<TEntity, TEntityDto>
    {
        public static TEntityDto toDto(TEntity entity, IMapper _mapper)
        {
            return _mapper.Map<TEntityDto>(entity);
        }

        public static ICollection<TEntityDto> toDto(ICollection<TEntity> entities, IMapper _mapper)
        {
            return (ICollection<TEntityDto>)(entities.Select(x => toDto(x, _mapper)));
        }

        public static PaginationDto<TEntityDto> paginationToDto(PaginationDto<TEntity> pagination, IMapper _mapper)
        {
            PaginationDto<TEntityDto> result = new PaginationDto<TEntityDto>
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = pagination.TotalPages,
                Entities = toDto(pagination.Entities, _mapper)
            };
            return result;
        }
    }
}
