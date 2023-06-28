using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Extensions;

public class BasketViewComponent : ViewComponent
{
    private readonly IShoppingBffService _basketService;

    public BasketViewComponent(IShoppingBffService basketService) => _basketService = basketService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _basketService.GetBasketCountAsync());
    }
}
