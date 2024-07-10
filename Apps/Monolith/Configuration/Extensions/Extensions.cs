using Bigpods.Monolith.Modules.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bigpods.Monolith.Config.Extensions;

public static class Extensions
{
    public static async Task ConfigureMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
            var context = services.GetRequiredService<DatabaseService>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(
                exception: ex,
                message: "An error occurred while migrating the database."
            );
        }
    }
}
