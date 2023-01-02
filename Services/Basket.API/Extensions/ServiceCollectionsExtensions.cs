using Basket.API.Repositories;

namespace Basket.API.Extensions;

/// <summary>
///     Contains application service collection.
/// </summary>
public static class ServiceCollectionsExtensions
{
    /// <summary>
    ///     Add and configure distributed redis system to application.
    /// </summary>
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });
    }
    
    /// <summary>
    /// Add and configure Swagger to application
    /// </summary>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    /// <summary>
    /// Realization of repository pattern 
    /// </summary>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBasketRepository, BasketRepository>();
    }
}