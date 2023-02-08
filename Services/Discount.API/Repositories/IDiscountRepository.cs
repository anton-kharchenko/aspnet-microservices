using Discount.API.Entities;

namespace Discount.API.Repositories;

/// <summary>
///  Deduction from the usual price of product. Working with Dapper and PostgresSQL.
///  API layer for communication between the application and DB. Implementation of Repository pattern.
/// </summary>
public interface IDiscountRepository
{
    /// <summary>
    /// Get the discount by the product name.
    /// </summary>
    /// <param name="productName">The name of product that needs to get discount.</param>
    /// <returns>The coupon object.</returns>
    Task<Coupon> GetDiscountAsync(string productName);

    /// <summary>
    /// Create the discount by the coupon.
    /// </summary>
    /// <param name="coupon">The coupon object that will be created.</param>
    /// <returns>The status of operation.</returns>
    Task<bool> CreateDiscountAsync(Coupon coupon);

    /// <summary>
    /// Update the discount by the coupon.
    /// </summary>
    /// <param name="coupon">The coupon object that will be updated.</param>
    /// <returns>The status of operation.</returns>
    Task<bool> UpdateDiscountAsync(Coupon coupon);

    /// <summary>
    /// Delete the discount by the product name.
    /// </summary>
    /// <param name="productName">The product name of coupon that will be deleted.</param>
    /// <returns>The status of operation.</returns>
    Task<bool> DeleteDiscountAsync(string productName);
}
