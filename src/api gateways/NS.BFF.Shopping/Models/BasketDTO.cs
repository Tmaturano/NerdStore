namespace NS.BFF.Shopping.Models;

public class BasketDTO
{
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
}