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
            CreateMap<LessonType, LessonTypeDto>();
            CreateMap<Lesson, LessonDto>();
            CreateMap<Record, RecordDto>();
            CreateMap<Event, EventDto>();
            CreateMap<EventType, EventTypeDto>();
            CreateMap<Work, WorkDto>();
            CreateMap<Activity, ActivityDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Institute, InstituteDto>();
            CreateMap<Job, JobDto>();
        }
    }
}
