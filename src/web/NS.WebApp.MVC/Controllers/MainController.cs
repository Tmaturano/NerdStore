using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool ResponseHasErrors(ResponseResult response)
    {
        if (response is not null && response.Errors.Messages.Any())
        {
            foreach (var error in response.Errors.Messages)
                ModelState.AddModelError(string.Empty, error);

            return true;
        }

        return false;
    }
}
