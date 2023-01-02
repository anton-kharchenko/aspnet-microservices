using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

/// <inheritdoc />
public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="redisCache">Redis distribute service cache. Get from Microsoft.Extensions.Caching.StackExchangeRedis</param>
    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    /// <inheritdoc />
    public async Task<ShoppingCart?> GetBasketAsync(string userName)
    {
       var basket =  await _redisCache.GetStringAsync(userName).ConfigureAwait(false);
       return string.IsNullOrWhiteSpace(basket)
           ? null
           : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    /// <inheritdoc />
    public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket)
    {
        await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket)).ConfigureAwait(false);
        return await GetBasketAsync(basket.UserName).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteBasketAsync(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
}