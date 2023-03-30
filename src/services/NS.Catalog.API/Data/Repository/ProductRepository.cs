using Microsoft.EntityFrameworkCore;
using NS.Catalog.API.Models;
using NS.Core.Data;

namespace NS.Catalog.API.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext _catalogContext;

    public ProductRepository(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public IUnitOfWork UnitOfWork => _catalogContext;

    public async Task<IEnumerable<Product>> GetAllAsync() => await _catalogContext.Products.AsNoTracking().ToListAsync();

    public async Task<Product> GetAsync(Guid id) => await _catalogContext.Products.FindAsync(id);

    public async Task AddAsync(Product product) => await _catalogContext.Products.AddAsync(product);

    public void Update(Product product) => _catalogContext.Products.Update(product);

    public void Dispose() => _catalogContext?.Dispose();
}
