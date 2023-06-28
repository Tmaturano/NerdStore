namespace NS.WebApp.MVC.Models;

public class BasketViewModel
{
    public decimal TotalPrice { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
}

public class BasketItemViewModel
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
}
