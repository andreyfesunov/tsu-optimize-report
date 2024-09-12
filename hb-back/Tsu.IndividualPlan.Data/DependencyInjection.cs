using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tsu.IndividualPlan.Data.Context;
using Tsu.IndividualPlan.Data.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;

namespace Tsu.IndividualPlan.Data;

public static class DependencyInjection
{
    public static void AddDataServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<IActivityEventTypeRepository, ActivityEventTypeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventTypeRepository, EventTypeRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<ILessonTypeRepository, LessonTypeRepository>();
        services.AddScoped<IWorkRepository, WorkRepository>();
        services.AddScoped<IRecordRepository, RecordRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IStateUserRepository, StateUserRepository>();
    }
}