using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName)
    {
       var basket =  await _redisCache.GetStringAsync(userName).ConfigureAwait(false);
       return string.IsNullOrWhiteSpace(basket)
           ? null
           : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket)
    {
        await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket)).ConfigureAwait(false);
        return await GetBasketAsync(basket.UserName).ConfigureAwait(false);
    }

    public async Task DeleteBasketAsync(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
}