using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Interfaces;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ShoppingController : ControllerBase
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;
    private readonly IOrderService _orderService;

    public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
    {
        _catalogService = catalogService;
        _basketService = basketService;
        _orderService = orderService;
    }

    [HttpGet("{userName}", Name = "GetShopping")]
    [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
    {
        // get basket with username
        // iterate basket items and consume products with basket item productId member
        // map product related members into basket item dto with extended columns
        // consume ordering microservices in order to retrieve order list
        // return root ShoppngModel dto class which including all responses

        var basket = await _basketService.GetBasket(userName).ConfigureAwait(false);

        foreach (var item in basket!.Items)
        {
            var product = await _catalogService.GetCatalog(item.ProductId).ConfigureAwait(false);

            // set additional product fields onto basket item
            item.ProductName = product.Name;
            item.Category = product.Category;
            item.Summary = product.Summary;
            item.Description = product.Description;
            item.ImageFile = product.ImageFile;
        }

        var orders = await _orderService.GetOrdersByUserName(userName).ConfigureAwait(false);

        var shoppingModel = new ShoppingModel {
            UserName = userName,
            BasketWithProducts = basket,
            Orders = orders
        };

        return Ok(shoppingModel);
    }
}
