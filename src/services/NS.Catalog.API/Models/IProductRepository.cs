using NS.Core.Data;

namespace NS.Catalog.API.Models;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetAsync(Guid id);
    Task AddAsync(Product product);
    void Update(Product product);
}
