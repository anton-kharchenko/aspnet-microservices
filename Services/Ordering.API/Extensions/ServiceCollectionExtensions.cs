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
}
