using Basket.API.Repositories;
using Basket.API.Services;
using Discount.gRPC.Protos;

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

    /// <summary>
    /// Register gRPC client project for communicate Basket.API with Discount.API
    /// </summary>
    public static void AddGrpcClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o=>
            o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]!)
        );
        services.AddScoped<DiscountGrpcService>();
    }
}