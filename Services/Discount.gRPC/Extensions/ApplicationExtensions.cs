using Discount.gRPC.Services;

namespace Discount.gRPC.Extensions;

public static class ApplicationExtensions
{
    
    public static void AddRpcServices(this IEndpointRouteBuilder app)
    {
        app.MapGrpcService<DiscountService>();
    }
    
    public static void AddFirstMigrate(this IHost app)
    {
        app.MigrateDatabase<Program>();
    }
}