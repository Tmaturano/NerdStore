using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Controllers;

[Authorize]
public class BasketController : MainController
{
    private readonly IBasketService _basketService;
    private readonly ICatalogService _catalogService;

    public BasketController(IBasketService basketService, ICatalogService catalogService)
    {
        _basketService = basketService;
        _catalogService = catalogService;
    }

    [Route("basket")]
    [HttpGet]
    public async Task<IActionResult> Index() => View(await _basketService.GetBasketAsync());

    [HttpPost]
    [Route("basket/add-item")]
    public async Task<IActionResult> AddItemBasket(ProductItemViewModel productItem)
    {
        var product = await _catalogService.GetByIdAsync(productItem.ProductId);

        ValidateItemBasket(product, productItem.Quantity);
        if (!IsOperationValid()) return View("Index", await _basketService.GetBasketAsync());

        productItem.Name = product.Name;
        productItem.Price = product.Price;
        productItem.Image = product.Image;

        var response = await _basketService.AddItemBasketAsync(productItem);

        if (ResponseHasErrors(response)) return View("Index", await _basketService.GetBasketAsync());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("basket/update-item")]
    public async Task<IActionResult> UpdateItemBasket(Guid productId, int quantity)
    {
        var product = await _catalogService.GetByIdAsync(productId);

        ValidateItemBasket(product, quantity);
        if (!IsOperationValid()) return View("Index", await _basketService.GetBasketAsync());

        var productItem = new ProductItemViewModel{ ProductId = productId, Quantity = quantity };
        var resposta = await _basketService.UpdateItemBasketAsync(productId, productItem);

        if (ResponseHasErrors(resposta)) return View("Index", await _basketService.GetBasketAsync());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("basket/remove-item")]
    public async Task<IActionResult> RemoveItemBasket(Guid productId)
    {
        var product = await _catalogService.GetByIdAsync(productId);

        if (product is null)
        {
            AddValidationError("Product not found!");
            return View("Index", await _basketService.GetBasketAsync());
        }

        var response = await _basketService.RemoveItemBasketAsync(productId);

        if (ResponseHasErrors(response)) return View("Index", await _basketService.GetBasketAsync());

        return RedirectToAction("Index");
    }

    private void ValidateItemBasket(ProductViewModel product, int quantity)
    {
        if (product is null) AddValidationError("Product not found!");
        if (quantity < 1) AddValidationError($"Select at least one unit of the product {product?.Name}");
        if (quantity > product?.StockQuantity) AddValidationError($"The product {product?.Name} has {product?.StockQuantity} units in stock, you selected {quantity}");
    }
}
