using Discount.gRPC.Repositories;

namespace Discount.gRPC.Extensions;

/// <summary>
///  The static extension class that contains the methods that work with IServiceCollection.
///  The custom services that will be added to the application 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registration of the discount repository pattern 
    /// </summary>
    /// <param name="services">The Services collection that existed in application</param>
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
    }

    public static void AddMapper(this IServiceCollection services)
    {
        services.AddMapper();
    }
}