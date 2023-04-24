using NS.Core.Messages;

namespace NS.Clients.API.Application.Events;

public class ClientAddedEvent : Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public ClientAddedEvent(Guid id, string name, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }
}
