using Basket.API.Repositories;

namespace Basket.API.Extensions;

public static class ServiceCollectionsExtensions
{
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });
    }
    
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBasketRepository, BasketRepository>();
    }
}