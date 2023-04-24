using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.Catalog.API.Models;
using NS.WebApi.Core.Controllers;
using NS.WebApi.Core.Identity;

namespace NS.Catalog.API.Controllers;

[Route("api/catalog")]
[Authorize]
public class CatalogController : MainController
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository) => _productRepository = productRepository;

    [AllowAnonymous]
    [HttpGet("products")]    
    public async Task<IEnumerable<Product>> GetProducts()
        => await _productRepository.GetAllAsync();

    [ClaimsAuthorize("Catalog", "Read")] 
    [HttpGet("products/{id}")]
    public async Task<Product> GetProductDetail(Guid id)
        => await _productRepository.GetAsync(id);
}
