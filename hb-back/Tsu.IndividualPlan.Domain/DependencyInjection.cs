using Microsoft.Extensions.DependencyInjection;
using Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Interfaces.Utils;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.Domain.Security;
using Tsu.IndividualPlan.Domain.Services;

namespace Tsu.IndividualPlan.Domain;

public static class DependencyInjection
{
    public static void AddDomainServices(
        this IServiceCollection services
    )
    {
        services.AddUser();
        services.AddServices();
        services.AddSecurity();
        services.AddStorage();
    }

    private static void AddUser(this IServiceCollection services)
    {
        services.AddScoped<UserInfo>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventTypeService, EventTypeService>();
        services.AddScoped<ILessonTypeService, LessonTypeService>();
        services.AddScoped<IStateService, StateService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IWorkService, WorkService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IRecordService, RecordService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IStateUserService, StateUserService>();
        services.AddScoped<IFileService, FileService>();
    }

    private static void AddSecurity(this IServiceCollection services)
    {
        services.AddScoped<IEventSecurityService, EventSecurityService>();
        services.AddScoped<ILessonSecurityService, LessonSecurityService>();
        services.AddScoped<ICommentSecurityService, CommentSecurityService>();
    }

    private static void AddStorage(this IServiceCollection services)
    {
        services.AddScoped<IStorage, FileService>();
    }
}