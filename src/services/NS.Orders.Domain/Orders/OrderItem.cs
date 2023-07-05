using NS.Core.DomainObjects;

namespace NS.Orders.Domain.Orders;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitaryValue { get; private set; }
    public string ProductImage { get; set; }

    // EF Rel.
    public Order Order { get; set; }

    public OrderItem(Guid productId, string productName, int quantity, 
        decimal unitaryValue, string productImage = null)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitaryValue = unitaryValue;
        ProductImage = productImage;
    }

    // EF ctor
    protected OrderItem() { }

    internal decimal CalculateValue() => Quantity * UnitaryValue;
}