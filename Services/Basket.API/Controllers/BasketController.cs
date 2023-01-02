using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{userName}", Name = "GetBasketAsync")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasketAsync(string userName)
    {
       var basket =  await _repository.GetBasketAsync(userName).ConfigureAwait(false);
       return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasketAsync([FromBody] ShoppingCart basket)
    {
        return Ok(await _repository.UpdateBasketAsync(basket).ConfigureAwait(false));
    }
    
    [HttpDelete("{userName}", Name = "DeleteBasketAsync")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> DeleteBasketAsync(string userName)
    {
         await _repository.DeleteBasketAsync(userName).ConfigureAwait(false);
         return Ok();
    }
}