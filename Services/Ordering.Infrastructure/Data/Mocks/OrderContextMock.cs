using Ordering.Domain.Entities;
using Ordering.Infrastructure.Data.Context;

namespace Ordering.Infrastructure.Data.Mocks;

public static class OrderContextMock
{
    public static async Task MockDataAsync(OrderContext context)
    {
        if (!context.Orders!.Any())
        {
            context.Orders!.AddRange(GetPreconfiguredOrders());
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new()
            {
                UserName = "Anton",
                FirstName = "Anton",
                LastName = "Kharhcenko",
                EmailAddress = "gumb1t97@gmail.com",
                AddressLine = "Sofia",
                Country = "Bulguria",
                TotalPrice = 350
            }
        };
    }
}
