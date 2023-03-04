using Shopping.Aggregator.Interfaces;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient) => _httpClient = httpClient;

    public Task<BasketModel> GetBasket(string userName) => throw new NotImplementedException();
}
