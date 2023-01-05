using Discount.API.Repositories;

namespace Discount.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepository(this  IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
    }
}