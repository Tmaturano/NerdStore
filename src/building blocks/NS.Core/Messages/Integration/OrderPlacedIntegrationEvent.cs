namespace NS.Core.Messages.Integration
{
    public class OrderPlacedIntegrationEvent : IntegrationEvent
    {
        public Guid ClientId { get; private set; }

        public OrderPlacedIntegrationEvent(Guid clientId) => ClientId = clientId;
    }
}