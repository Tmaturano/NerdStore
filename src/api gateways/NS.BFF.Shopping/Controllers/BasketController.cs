using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.WebApi.Core.Controllers;

namespace NS.Bff.Shopping.Controllers
{
    [Route("api/shopping")]
    [Authorize]
    public class BasketController : MainController
    {
        [HttpGet("basket")]        
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet("basket/basket-count")]        
        public async Task<IActionResult> GetBasketCount()
        {
            return CustomResponse();
        }

        [HttpPost("basket/items")]        
        public async Task<IActionResult> AddBasketItem()
        {
            return CustomResponse();
        }

        [HttpPut("basket/items/{productId}")]        
        public async Task<IActionResult> UpdateBasketItem()
        {
            return CustomResponse();
        }

        [HttpDelete("basket/items/{productId}")]        
        public async Task<IActionResult> RemoveBasketItem()
        {
            return CustomResponse();
        }
    }
}
