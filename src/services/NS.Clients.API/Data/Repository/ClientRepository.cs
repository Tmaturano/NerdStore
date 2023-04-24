using Microsoft.EntityFrameworkCore;
using NS.Clients.API.Models;
using NS.Core.Data;

namespace NS.Clients.API.Data.Repository;

public class ClientRepository : IClientRepository
{
    private readonly ClientContext _context;

    public ClientRepository(ClientContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task AddAsync(Client client) => await _context.Clients.AddAsync(client);

    public async Task<IEnumerable<Client>> GetAllAsync() => await _context.Clients.AsNoTracking().ToListAsync();

    public async Task<Client> GetAsync(int id) => await _context.Clients.FindAsync(id);

    public async Task<Client> GetByCPFASync(string cpf) => await _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);

    public void Dispose() => _context?.Dispose();
}
