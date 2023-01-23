using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions;

public static class HostExtensions
{
    public static async Task MigrateDatabase<TContext>(
        this IHost host,
        Action<TContext, IServiceProvider> mocker,
        int? retry = 0
    ) where TContext : DbContext
    {
        var retryForAvailability = retry!.Value;

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetService<ILogger<TContext>>();
        var context = services.GetService<TContext>();
        try
        {
            logger!.LogInformation("Migrating db associated with context {DbContextNam}", typeof(TContext));

           await InvokeMockAsync(mocker!, context, services).ConfigureAwait(false);

            logger!.LogInformation("Migrated db associated with context {DbContextNam}", typeof(TContext));
        }
        catch (SqlException e)
        {
            logger!.LogError(e, "An error while migrating db used on context");
            if (retryForAvailability< 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                await MigrateDatabase(host, mocker, retryForAvailability).ConfigureAwait(false);
            }
        }
    }

    private static async Task InvokeMockAsync<TContext>(Action<TContext, IServiceProvider> mocker, TContext context, IServiceProvider services) where TContext : DbContext?
    {
        await context!.Database.MigrateAsync().ConfigureAwait(false);
        mocker(context, services);
    }
}
