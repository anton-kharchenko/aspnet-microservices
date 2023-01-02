using Basket.API.Entities;

namespace Basket.API.Repositories;

/// <summary>
/// API layer for communication between the application and redis distributed db. Implementation of Repository pattern.
/// </summary>
public interface IBasketRepository
{
    /// <summary>
    /// Get the basket by the user name from redis distributed db.
    /// </summary>
    /// <param name="userName">The user name</param>
    /// <returns>The user's basket</returns>
    Task<ShoppingCart?> GetBasketAsync(string userName);
    
    /// <summary>
    /// Update the basket
    /// </summary>
    /// <param name="basket">The new basket</param>
    /// <returns>The updated basket</returns>
    Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket);
    
    /// <summary>
    /// Delete the basket by the user name from redis distributed db. 
    /// </summary>
    /// <param name="userName">The user name</param>
    Task DeleteBasketAsync(string userName);
}