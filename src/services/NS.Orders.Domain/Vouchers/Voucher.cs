using NS.Core.DomainObjects;
using NS.Orders.Domain.Specs;

namespace NS.Orders.Domain;

public class Voucher : Entity, IAggregateRoot
{
    public string Code { get; private set; }
    public decimal? Percentage { get; private set; }
    public decimal? DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public VoucherDiscountType DiscountType { get; private set; }
    public DateTime RegisterDate { get; private set; }
    public DateTime? UseDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; private set; }
    public bool Used { get; private set; }

    public bool IsValidForUse()
    {
        return new VoucherActiveSpecification()
            .And(new VoucherDateSpecification())
            .And(new VoucherQuantitySpecification())
            .IsSatisfiedBy(this);
    }

    public void SetAsUsed()
    {
        IsActive = false;
        Used = true;
        Quantity = 0;
        UseDate = DateTime.Now;
    }

    public void DebitAmount()
    {
        Quantity -= 1;
        if (Quantity >= 1) return;

        SetAsUsed();
    }
}