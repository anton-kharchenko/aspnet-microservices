using System.Net;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

/// <summary>
/// Discount controller. Endpoints for interaction with application.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _discountRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="discountRepository">PostgresSQL repository</param>
    public DiscountController(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    
    /// <summary>
    /// Get discount by product name 
    /// </summary>
    /// <param name="productName">The name of product that need to get discount</param>
    /// <returns>The coupon</returns>
    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> GetDiscountAsync(string productName)
    {
        return Ok(await _discountRepository.GetDiscountAsync(productName));
    }

    /// <summary>
    /// Create a new coupon.
    /// </summary>
    /// <param name="coupon">A new coupon that should be created. Gets from body of request.</param>
    /// <returns>A new coupon.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> CreateDiscountAsync([FromBody] Coupon coupon)
    {
        await _discountRepository.CreateDiscountAsync(coupon);
        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }

    /// <summary>
    ///  Update the existed coupon.
    /// </summary>
    /// <param name="coupon">The coupon that should be updated. Gets from body of request.</param>
    /// <returns>The updated coupon</returns>
    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscountAsync([FromBody] Coupon coupon)
    {
        return Ok(await _discountRepository.UpdateDiscountAsync(coupon));
    }

    /// <summary>
    /// Delete the discount by product name 
    /// </summary>
    /// <param name="productName">The name of product that will be deleted discount</param>
    /// <returns>Status of operation</returns>
    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteDiscountAsync(string productName)
    {
        return Ok(await _discountRepository.DeleteDiscountAsync(productName));
    }
}