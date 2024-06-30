using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services
{
    public class DepartmentService : IDepartmentService
    {
        protected readonly IMapper _mapper;
        protected MappingHelper<Department, DepartmentDto> _mappingHelper;
        protected IBaseRepository<Department> _repository;

        public DepartmentService(DepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mappingHelper = new MappingHelper<Department, DepartmentDto>(_mapper);
        }

        public async Task<Department> AddEntity(Department entity)
        {
            return await _repository.AddEntity(entity);
        }

        public async Task<DepartmentDto> GetById(Guid id)
        {
            return _mappingHelper.toDto(await _repository.GetById(id));
        }

        public async Task<ICollection<DepartmentDto>> GetAll()
        {
            return _mappingHelper.toDto(await _repository.GetAll());
        }

        public async Task<Department> Update(Department entity)
        {
            return await _repository.UpdateEntity(entity);
        }

        public async Task<bool> DeleteById(Guid entityId)
        {
            return await _repository.DeleteById(entityId);
        }

        public async Task<PaginationDto<DepartmentDto>> Search(SearchDto searchDto)
        {
            return _mappingHelper.paginationToDto(await _repository.Search(searchDto));
        }
    }
}
