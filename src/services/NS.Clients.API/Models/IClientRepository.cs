using NS.Core.Data;

namespace NS.Clients.API.Models;

public interface IClientRepository : IRepository<Client>
{
    Task AddAsync(Client client);
    Task<Client> GetAsync(int id);
    Task<Client> GetByCPFASync(string cpf);
    Task<IEnumerable<Client>> GetAllAsync();
}
