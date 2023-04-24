using MediatR;

namespace NS.Clients.API.Application.Events;

public class ClientEventHandler : INotificationHandler<ClientAddedEvent>
{
    public Task Handle(ClientAddedEvent notification, CancellationToken cancellationToken)
    {
        //send a confirmation event. implement a business rule that makes sense.        
        return Task.CompletedTask;
    }
}
