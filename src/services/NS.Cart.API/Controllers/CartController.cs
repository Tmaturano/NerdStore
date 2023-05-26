using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.WebApi.Core.Controllers;

namespace NS.Cart.API.Controllers
{
    [Route("api/cart")]
    [Authorize]
    public class CartController : MainController
    {
    }
}
