using NS.Core.DomainObjects;

namespace NS.Clients.API.Models;

public class Address : Entity
{
    public Address(string street, string city, string state, string postalCode, string number, string neighborhood, string complement)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Number = number;
        Neighborhood = neighborhood;
        Complement = complement;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string Complement { get; private set; }
    public Guid ClientId { get; private set; }
    public Client Client{ get; protected set; }
}
