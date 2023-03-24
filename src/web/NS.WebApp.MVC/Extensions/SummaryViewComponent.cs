using Microsoft.AspNetCore.Mvc;

namespace NS.WebApp.MVC.Extensions;

public class SummaryViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View();
}
