using NS.Core.Messages;

namespace NS.Orders.API.Application.Events
{
    public class OrderPlacedEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }

        public OrderPlacedEvent(Guid orderId, Guid clientId)
        {
            OrderId = orderId;
            ClientId = clientId;
        }
    }
}