namespace Shopping.Aggregator.Models;

public class BasketModel
{
    public string UserName { get; set; } = default!;
    public List<BasketItemExtendedModel> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
}
