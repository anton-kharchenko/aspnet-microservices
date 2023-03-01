namespace Shopping.Aggregator.Models;

public class BasketItemExtendedModel
{
    public int Quantity { get; set; }
    public string Color { get; set; } = default!;
    public decimal Price { get; set; }
    public string ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string Category { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
}
