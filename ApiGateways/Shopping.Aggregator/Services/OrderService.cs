using Shopping.Aggregator.Interfaces;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient) => _httpClient = httpClient;

    public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName) => throw new NotImplementedException();
}
