using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Controllers;

[Authorize]
public class BasketController : MainController
{
    private readonly IShoppingBffService _basketService;

    public BasketController(IShoppingBffService basketService) => _basketService = basketService;

    [Route("basket")]
    [HttpGet]
    public async Task<IActionResult> Index() => View(await _basketService.GetBasketAsync());

    [HttpPost]
    [Route("basket/add-item")]
    public async Task<IActionResult> AddItemBasket(BasketItemViewModel productItem)
    {       
        var response = await _basketService.AddItemBasketAsync(productItem);

        if (ResponseHasErrors(response)) return View("Index", await _basketService.GetBasketAsync());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("basket/update-item")]
    public async Task<IActionResult> UpdateItemBasket(Guid productId, int quantity)
    {       
        var productItem = new BasketItemViewModel{ ProductId = productId, Quantity = quantity };
        var resposta = await _basketService.UpdateItemBasketAsync(productId, productItem);

        if (ResponseHasErrors(resposta)) return View("Index", await _basketService.GetBasketAsync());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("basket/remove-item")]
    public async Task<IActionResult> RemoveItemBasket(Guid productId)
    {        
        var response = await _basketService.RemoveItemBasketAsync(productId);

        if (ResponseHasErrors(response)) return View("Index", await _basketService.GetBasketAsync());

        return RedirectToAction("Index");
    }
}
