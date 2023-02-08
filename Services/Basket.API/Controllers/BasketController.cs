﻿using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Basket.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

/// <summary>
///     Basket controller. Endpoints for interaction with application.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;
    private readonly DiscountGrpcService _discountGrpcService;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="repository">Redis distributed caching repository</param>
    /// <param name="discountGrpcService">Discount service for communicate with Discount.API. Contains methods that generated by gRPC Client</param>
    public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
    {
        _repository = repository;
        _discountGrpcService = discountGrpcService;
    }

    /// <summary>
    /// Get basket by user name.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <returns>Shopping cart.</returns>
    [HttpGet("{userName}", Name = "GetBasketAsync")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasketAsync(string? userName)
    {
       var basket =  await _repository.GetBasketAsync(userName).ConfigureAwait(false);
       return Ok(basket ?? new ShoppingCart(userName));
    }

    /// <summary>
    /// Create basket if it's exist. Update basket if it exist. Also return price with discount if it has.
    /// </summary>
    /// <param name="basket">The basket that must be update.</param>
    /// <returns>Shopping cart.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasketAsync([FromBody] ShoppingCart basket)
    {
        foreach (var shoppingCartItem in basket.Items)
        {
            var coupon = await _discountGrpcService.GetDiscountAsync(shoppingCartItem.ProductName).ConfigureAwait(false);
            shoppingCartItem.Price -= coupon.Amount;
        }

        return Ok(await _repository.UpdateBasketAsync(basket).ConfigureAwait(false));
    }

    /// <summary>
    /// Delete basket.
    /// </summary>
    /// <param name="userName">The user name.</param>
    [HttpDelete("{userName}", Name = "DeleteBasketAsync")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> DeleteBasketAsync(string userName)
    {
         await _repository.DeleteBasketAsync(userName).ConfigureAwait(false);
         return Ok();
    }
}
