using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Interfaces;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<BasketModel?> GetBasket(string userName)
    {
        var response = await _httpClient.GetAsync($"/api/v1/Basket/{userName}").ConfigureAwait(false);
        return await response.ReadContentAs<BasketModel>().ConfigureAwait(false);
    }
}
