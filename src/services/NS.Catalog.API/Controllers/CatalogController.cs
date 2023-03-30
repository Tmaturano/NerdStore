using Microsoft.AspNetCore.Mvc;
using NS.Catalog.API.Models;

namespace NS.Catalog.API.Controllers;

[ApiController]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository) => _productRepository = productRepository;

    [HttpGet("products")]
    public async Task<IEnumerable<Product>> GetProducts()
        => await _productRepository.GetAllAsync();

    [HttpGet("products/{id}")]
    public async Task<Product> GetProductDetail(Guid id)
        => await _productRepository.GetAsync(id);
}
