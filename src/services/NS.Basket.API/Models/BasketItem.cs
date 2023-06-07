using FluentValidation;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public BasketClient BasketClient { get; set; }

    internal void AssociateBasket(Guid basketId) => BasketId = basketId;

    internal decimal CalculatePrice() => Quantity * Price;

    internal void AddUnits(int units) => Quantity += units;

    internal void UpdateUnits(int units) => Quantity = units; 

    internal bool IsValid() => new BasketItemValidation().Validate(this).IsValid;

    public class BasketItemValidation : AbstractValidator<BasketItem>
    {
        public BasketItemValidation()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product Id");

            RuleFor(c => c.Name)
                .NotEmpty();
            
            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage(item => $"The minimum quantity for {item.Name} is 1");

            RuleFor(c => c.Quantity)
                .LessThanOrEqualTo(BasketClient.MAX_QUANTITY_ITEM)
                .WithMessage(item => $"The maximum quantity of {item.Name} is {BasketClient.MAX_QUANTITY_ITEM}");

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage(item => $"The price of {item.Name} must be greater than 0");
        }
    }
}