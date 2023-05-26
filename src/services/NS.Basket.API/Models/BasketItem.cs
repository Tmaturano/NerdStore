namespace NS.Basket.API.Models;

public class BasketItem
{
    public BasketItem() => Id = Guid.NewGuid();

    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }

    public Guid BasketId { get; set; }

    public BasketClient BasketClient { get; set; }
}
