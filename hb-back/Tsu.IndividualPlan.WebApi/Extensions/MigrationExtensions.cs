using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Data;

namespace Tsu.IndividualPlan.WebApi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        dbContext.Database.Migrate();
    }
}