using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Extensions;

public class BasketViewComponent : ViewComponent
{
    private readonly IBasketService _basketService;

    public BasketViewComponent(IBasketService basketService) => _basketService = basketService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _basketService.GetBasketAsync() ?? new BasketViewModel());
    }
}
