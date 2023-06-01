using FluentValidation;
using FluentValidation.Results;

namespace NS.Basket.API.Models;

public class BasketClient
{
    internal const int MAX_QUANTITY_ITEM = 5;

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public ValidationResult ValidationResult { get; set; }

    public BasketClient(Guid clientId)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
    }

    public BasketClient() { }

    private void CalculateBasketTotalPrice() => TotalPrice = Items.Sum(p => p.CalculatePrice());

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
