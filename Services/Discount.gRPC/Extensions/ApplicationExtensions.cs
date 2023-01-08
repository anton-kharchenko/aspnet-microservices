using Discount.gRPC.Services;

namespace Discount.gRPC.Extensions;

/// <summary>
/// Contains application extensions methods.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    /// Register gRPC services. After build will be created additional classes for gRPC
    /// </summary>
    /// <param name="app">A route builder in an application</param>
    public static void AddRpcServices(this IEndpointRouteBuilder app)
    {
        app.MapGrpcService<DiscountService>();
    }
    
    /// <summary>
    /// Add first migrations before application connected with db.
    /// </summary>
    /// <param name="app">Contains application services, start and stop methods for the application.</param>
    public static void AddFirstMigrate(this IHost app)
    {
        app.MigrateDatabase<Program>();
    }
}