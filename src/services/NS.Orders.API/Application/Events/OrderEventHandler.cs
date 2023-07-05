using MediatR;
using NS.Core.Messages.Integration;
using NS.MessageBus;

namespace NS.Orders.API.Application.Events;

public class OrderEventHandler : INotificationHandler<OrderPlacedEvent>
{
    private readonly IMessageBus _bus;

    public OrderEventHandler(IMessageBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderPlacedEvent message, CancellationToken cancellationToken) 
        => await _bus.PublishAsync(new OrderPlacedIntegrationEvent(message.ClientId));
}