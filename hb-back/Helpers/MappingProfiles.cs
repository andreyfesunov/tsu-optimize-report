using AutoMapper;
using BackendBase.Dto;
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
        }
    }
}
