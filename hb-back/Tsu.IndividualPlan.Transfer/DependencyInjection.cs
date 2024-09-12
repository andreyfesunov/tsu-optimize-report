using Microsoft.Extensions.DependencyInjection;
using Tsu.IndividualPlan.Transfer.Interfaces.Report;
using Tsu.IndividualPlan.Transfer.Services.Report;

namespace Tsu.IndividualPlan.Transfer;

public static class DependencyInjection
{
    public static void AddTransferServices(this IServiceCollection services)
    {
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IReportCreateService, ReportCreateService>();
        services.AddScoped<IReportExportService, ReportExportService>();
    }
}