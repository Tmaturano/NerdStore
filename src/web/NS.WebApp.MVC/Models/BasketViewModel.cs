namespace NS.WebApp.MVC.Models;

public class BasketViewModel
{
    public decimal TotalPrice { get; set; }
    public List<ProductItemViewModel> Items { get; set; } = new List<ProductItemViewModel>();
}

public class ProductItemViewModel
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
}
