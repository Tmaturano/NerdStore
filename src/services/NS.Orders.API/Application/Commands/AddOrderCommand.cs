using FluentValidation;
using NS.Core.Messages;
using NS.Orders.API.Application.DTO;

namespace NS.Orders.API.Application.Commands;

public class AddOrderCommand : Command
{
    // Order
    public Guid ClientId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }

    // Voucher
    public string VoucherCode { get; set; }
    public bool VoucherAlreadyUsed { get; set; }
    public decimal Discount { get; set; }

    // Address
    public AddressDTO Address { get; set; }

    // Card
    public string CardNumber { get; set; }
    public string CardName { get; set; }
    public string ExpirationCard { get; set; }
    public string CardCvv { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new AddOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AddOrderValidation : AbstractValidator<AddOrderCommand>
    {
        public AddOrderValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Client Id");

            RuleFor(c => c.OrderItems.Count)
                .GreaterThan(0)
                .WithMessage("The order must have at least 1 item");

            RuleFor(c => c.TotalPrice)
                .GreaterThan(0)
                .WithMessage("Invalid order total price");

            RuleFor(c => c.CardNumber)
                .CreditCard()
                .WithMessage("Invalid Card Number");

            RuleFor(c => c.CardName)
                .NotNull()
                .WithMessage("Required cardholder name");

            RuleFor(c => c.CardCvv.Length)
                .GreaterThan(2)
                .LessThan(5)
                .WithMessage("The card CVV must have between 3 or 4 numbers");

            RuleFor(c => c.ExpirationCard)
                .NotNull()
                .WithMessage("Required Expiration card date");
        }
    }
}
