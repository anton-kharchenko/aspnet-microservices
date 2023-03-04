namespace Shopping.Aggregator.Models;

public class ShoppingModel
{
    public string UserName { get; set; } = default!;
    public BasketModel BasketWithProducts { get; set; } = default!;
    public IEnumerable<OrderResponseModel> Orders { get; set; } = default!;
}
