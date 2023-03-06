using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Interfaces;

public interface IBasketService
{
    Task<BasketModel?> GetBasket(string userName);
}
