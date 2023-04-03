using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Controllers;

public class CatalogController : MainController
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService) => _catalogService = catalogService;

    [HttpGet]
    [Route("")]
    [Route("showcase")]
    public async Task<IActionResult> Index()
    {
        var products = await _catalogService.GetAllAsync();
        return View(products);
    }

    [HttpGet]
    [Route("product-detail/{id}")]
    public async Task<IActionResult> ProductDetail(Guid id)
    {
        var productDetail = await _catalogService.GetByIdAsync(id);
        return View(productDetail);
    }
}
