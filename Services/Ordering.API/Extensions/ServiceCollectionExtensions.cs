using System.Reflection;
using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.EventBusConsumers;

namespace Ordering.API.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adding message broker to app
    /// </summary>
    [Obsolete("Obsolete")]
    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(
            config => {
                config.AddConsumer<BasketCheckoutConsumer>();
                config.UsingRabbitMq(
                    (ctx, cfg) => {
                        cfg.Host(configuration["EventBusSettings:HostAddress"]);
                        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
                            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                        });
                    }
                );
            }
        );

        services.AddMassTransitHostedService();
    }

    /// <summary>
    /// Adding mapping library
    /// </summary>
    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Add the consumers
    /// </summary>
    public static void AddConsumers(this IServiceCollection services)
    {
        services.AddScoped<BasketCheckoutConsumer>();
    }
}
