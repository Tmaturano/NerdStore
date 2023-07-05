using NS.Core.DomainObjects;

namespace NS.Orders.Domain.Orders;

public class Order : Entity, IAggregateRoot
{
    public Order(Guid clientId, decimal totalPrice, List<OrderItem> orderItems, 
        bool voucherAlreadyUsed = false, decimal discount = 0, Guid? voucherId = null)
    {
        ClientId = clientId;
        TotalPrice = totalPrice;
        _orderItems = orderItems;

        Discount = discount;
        VoucherAlreadyUsed = voucherAlreadyUsed;
        VoucherId = voucherId;
    }

    // EF ctor
    protected Order() { }

    public int Code { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherAlreadyUsed { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime RegisterDate { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    
    public Address Address { get; private set; }

    // EF Rel.
    public Voucher Voucher { get; private set; }

    public void AuthorizeOrder()
    {
        OrderStatus = OrderStatus.Authorized;
    }

    public void SetVoucher(Voucher voucher)
    {
        VoucherAlreadyUsed = true;
        VoucherId = voucher.Id;
        Voucher = voucher;
    }

    public void SetAddress(Address address) => Address = address;

    public void CalculateOrderValue()
    {
        TotalPrice = OrderItems.Sum(p => p.CalculateValue());
        CalculateTotalDiscount();
    }

    public void CalculateTotalDiscount()
    {
        if (!VoucherAlreadyUsed) return;

        decimal discount = 0;
        var value = TotalPrice;

        if (Voucher.DiscountType == VoucherDiscountType.Percentage)
        {
            if (Voucher.Percentage.HasValue)
            {
                discount = (value * Voucher.Percentage.Value) / 100;
                value -= discount;
            }
        }
        else
        {
            if (Voucher.DiscountValue.HasValue)
            {
                discount = Voucher.DiscountValue.Value;
                value -= discount;
            }
        }

        TotalPrice = value < 0 ? 0 : value;
        Discount = discount;
    }
}