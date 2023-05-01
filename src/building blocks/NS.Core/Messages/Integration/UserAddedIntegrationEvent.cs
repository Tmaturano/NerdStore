namespace NS.Core.Messages.Integration;

public class UserAddedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public UserAddedIntegrationEvent(Guid id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }
}
