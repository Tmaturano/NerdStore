using FluentValidation;
using FluentValidation.Results;
using NS.Basket.API.Model;

namespace NS.Basket.API.Models;

public class BasketClient
{
    internal const int MAX_QUANTITY_ITEM = 5;

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public ValidationResult ValidationResult { get; set; }

    public bool VoucherAlreadyUsed { get; set; }
    public decimal Discount { get; set; }
    public Voucher Voucher { get; set; }

    public BasketClient(Guid clientId)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
    }

    public BasketClient() { }

    internal void ApplyVoucher(Voucher voucher)
    {
        Voucher = voucher;
        VoucherAlreadyUsed = true;
        CalculateBasketTotalPrice();
    }

    private void CalculateDiscountTotalValue()
    {
        if (!VoucherAlreadyUsed) return;

        decimal discount = 0;
        var totalPrice = TotalPrice;

        if (Voucher.DiscountType == VoucherDiscountType.Percentage)
        {
            if (Voucher.Percentage.HasValue)
            {
                discount = (totalPrice * Voucher.Percentage.Value) / 100;
                totalPrice -= discount;
            }
        }
        else
        {
            if (Voucher.DiscountValue.HasValue)
            {
                discount = Voucher.DiscountValue.Value;
                totalPrice -= discount;
            }
        }

        TotalPrice = totalPrice < 0 ? 0 : totalPrice;
        Discount = discount;
    }

    private void CalculateBasketTotalPrice()
    {
        TotalPrice = Items.Sum(p => p.CalculatePrice());
        CalculateDiscountTotalValue();
    }

    internal bool BasketItemExists(BasketItem item) => Items.Any(p => p.ProductId == item.ProductId);

    internal BasketItem GetItemByProductId(Guid productId) => Items.FirstOrDefault(p => p.ProductId == productId);

    internal void AddItem(BasketItem item)
    {
        item.AssociateBasket(item.BasketId);

        if (BasketItemExists(item))
        {
            //update item quantity in case it already exists in the basket
            var existingItem = GetItemByProductId(item.ProductId);
            existingItem.AddUnits(item.Quantity);

            item = existingItem;
            Items.Remove(existingItem);
        }
                
        Items.Add(item);
        CalculateBasketTotalPrice();
    }

    internal void UpdateItem(BasketItem item)
    {
        item.AssociateBasket(Id);

        var itemExistente = GetItemByProductId(item.ProductId);

        Items.Remove(itemExistente);
        Items.Add(item);

        CalculateBasketTotalPrice();
    }

    internal void UpdateUnits(BasketItem item, int units)
    {
        item.UpdateUnits(units);
        UpdateItem(item);
    }

    internal void RemoveItem(BasketItem item)
    {
        Items.Remove(GetItemByProductId(item.ProductId));
        CalculateBasketTotalPrice();
    }

    internal bool IsValid()
    {
        var errors = Items.SelectMany(i => new BasketItem.BasketItemValidation().Validate(i).Errors).ToList();
        errors.AddRange(new BasketClientValidation().Validate(this).Errors);
        ValidationResult = new ValidationResult(errors);

        return ValidationResult.IsValid;
    }

    public class BasketClientValidation : AbstractValidator<BasketClient>
    {
        public BasketClientValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Client not found");

            RuleFor(c => c.Items.Count)
                .GreaterThan(0)
                .WithMessage("No items in the basket");

            RuleFor(c => c.TotalPrice)
                .GreaterThan(0)
                .WithMessage("The basket total price must be greater than 0");
        }
    }
}
