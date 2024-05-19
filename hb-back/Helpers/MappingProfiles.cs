using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Dto.Report;
using BackendBase.Models;
using BackendBase.Models.Enum;

namespace BackendBase.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, RegistrationDto>();
        CreateMap<RegistrationDto, User>();
        CreateMap<StateUser, ReportListDto>().ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src =>
                src.State.EndDate <= DateTime.Now ? StateUserStatus.Finished :
                src.Records.Count > 0 ? StateUserStatus.Active : StateUserStatus.NotActive)
        );
        CreateMap<ReportListDto, StateUser>();
        CreateMap<State, StateDto>();
        CreateMap<StateDto, State>();
        CreateMap<LessonType, LessonTypeDto>();
        CreateMap<Lesson, LessonDto>();
        CreateMap<Record, RecordDto>();
        CreateMap<Event, EventDto>();
        CreateMap<EventType, EventTypeDto>();
        CreateMap<ActivityEventType, ActivityEventTypeDto>();
        CreateMap<Work, WorkDto>();
        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityDto, Activity>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentDto, Department>();
        CreateMap<Institute, InstituteDto>();
        CreateMap<Job, JobDto>();
        CreateMap<JobDto, Job>();
        CreateMap<Rank, RankDto>();
        CreateMap<Degree, DegreeDto>();

        CreateMap<StateCreateDto, State>();
        CreateMap<State, StateCreateDto>();
        CreateMap<StateUser, StateUserCreateDto>();
        CreateMap<StateUserCreateDto, StateUser>();
        CreateMap<EventTypeAssignDto, ActivityEventType>();
    }
}