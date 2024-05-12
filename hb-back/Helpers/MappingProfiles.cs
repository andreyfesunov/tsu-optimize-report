using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Report;
using BackendBase.Models;

namespace BackendBase.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, RegistrationDto>();
            CreateMap<RegistrationDto, User>();
            CreateMap<StateUser, ReportListDto>();
            CreateMap<State, StateDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Institute, InstituteDto>();
            CreateMap<Job, JobDto>();
        }
    }
}
