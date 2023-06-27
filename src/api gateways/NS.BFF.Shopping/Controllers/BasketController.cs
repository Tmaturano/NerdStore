using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.Bff.Shopping.Services;
using NS.WebApi.Core.Controllers;
using NSE.Bff.Compras.Services;

namespace NS.Bff.Shopping.Controllers
{
    [Route("api/shopping")]
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

        [HttpGet("basket")]        
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet("basket/count")]        
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
