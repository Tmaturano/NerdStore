using NS.Core.Messages;

namespace NS.Clients.API.Application.Commands;

public class AddClientCommand : Command
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string CPF { get; private set; }

    public AddClientCommand(Guid id, string name, string email, string cPF)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Email = email;
        CPF = cPF;
    }
}
