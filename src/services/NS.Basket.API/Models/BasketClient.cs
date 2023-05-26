namespace NS.Basket.API.Models;

public class BasketClient
{
    internal const int MAX_QUANTIDADE_ITEM = 5;

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    public BasketClient(Guid clientId)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
    }

    protected BasketClient() { }
}
