using Npgsql;

namespace Discount.API.Extensions;

/// <summary>
/// The static extension class that contains the methods that work with IHost. 
/// </summary>
public static class HostExtensions
{
    /// <summary>
    /// Create first migration for PostgresSQL if any coupons doesn't exist.
    /// Will clear the DB and then add the 2 base coupons rows 
    /// </summary>
    /// <param name="host">The interface that will be extended. Executed when the application is starting.</param>
    /// <param name="retry">The counter of trying to execute first migration. Counter must be less that 50.</param>
    /// <typeparam name="TContext">The class that will be logging</typeparam>
    public static void MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        var retryForAvailability = retry!.Value;

        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Migrating postgres started"); 
            using var connection = new NpgsqlConnection
                (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };
            
            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
            command.ExecuteNonQuery();

            logger.LogInformation("Migrated postgres database.");

        }
        catch (NpgsqlException e)
        {
            logger.LogError(e, "An error occurred while migrating the postgres database");
            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, retryForAvailability);
            }
        }
    }
}