using FluentValidation.Results;
using MediatR;
using NS.Core.Messages;
using NS.Orders.API.Application.DTO;
using NS.Orders.API.Application.Events;
using NS.Orders.Domain;
using NS.Orders.Domain.Orders;
using NS.Orders.Domain.Specs;

namespace NS.Orders.API.Application.Commands;

public class OrderCommandHandler : CommandHandler, IRequestHandler<AddOrderCommand, ValidationResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IVoucherRepository _voucherRepository;

    public OrderCommandHandler(IVoucherRepository voucherRepository,
                                IOrderRepository orderRepository)
    {
        _voucherRepository = voucherRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ValidationResult> Handle(AddOrderCommand message, CancellationToken cancellationToken)
    {        
        if (!message.IsValid()) return message.ValidationResult;
                
        var order = MapOrder(message);
                
        if (!await ApplyVoucher(message, order)) return ValidationResult;
                
        if (!ValidateOrder(order)) return ValidationResult;
                
        if (!ProcessPayment(order)) return ValidationResult;
                
        order.AuthorizeOrder();
                
        order.AddEvent(new OrderPlacedEvent(order.Id, order.ClientId));
                
        _orderRepository.Add(order);
                
        return await PersistData(_orderRepository.UnitOfWork);
    }

    private Order MapOrder(AddOrderCommand message)
    {
        var address = new Address
        {
            Street = message.Address.Street,
            Number = message.Address.Number,
            Complement = message.Address.Complement,
            Neighborhood = message.Address.Neighborhood,
            ZipCode = message.Address.ZipCode,
            City = message.Address.City,
            State = message.Address.State
        };

        var order = new Order(message.ClientId, message.TotalPrice, message.OrderItems.Select(OrderItemDTO.ToOrderItem).ToList(),
            message.VoucherAlreadyUsed, message.Discount);

        order.SetAddress(address);
        return order;
    }

    private async Task<bool> ApplyVoucher(AddOrderCommand message, Order order)
    {
        if (!message.VoucherAlreadyUsed) return true;

        var voucher = await _voucherRepository.GetVoucherByCodeAsync(message.VoucherCode);
        if (voucher == null)
        {
            AddError("The voucher was not found!");
            return false;
        }

        var voucherValidation = new VoucherValidation().Validate(voucher);
        if (!voucherValidation.IsValid)
        {
            voucherValidation.Errors.ToList().ForEach(m => AddError(m.ErrorMessage));
            return false;
        }

        order.SetVoucher(voucher);
        voucher.DebitAmount();

        _voucherRepository.Update(voucher);

        return true;
    }

    private bool ValidateOrder(Order order)
    {
        var orderOriginalPrice = order.TotalPrice;
        var orderDiscount = order.Discount;

        order.CalculateOrderValue();

        if (order.TotalPrice != orderOriginalPrice)
        {
            AddError("The order total value does not match the calculation of the order");
            return false;
        }

        if (order.Discount != orderDiscount)
        {
            AddError("The total price does not match the calculation of the order");
            return false;
        }

        return true;
    }

    public bool ProcessPayment(Order order)
    {
        return true;
    }
}
