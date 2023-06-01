using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NS.Basket.API.Data;
using NS.Basket.API.Models;
using NS.WebApi.Core.Controllers;
using NS.WebApi.Core.User;

namespace NS.Basket.API.Controllers
{
    [Route("api/basket")]
    [Authorize]
    public class BasketController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly BasketContext _context;

        public BasketController(IAspNetUser user, BasketContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("basket")]
        public async Task<BasketClient> Get() => await GetBasketClientAsync() ?? new BasketClient();

        [HttpPost("basket")]
        public async Task<IActionResult> Post(BasketItem item)
        {
            var basket = await GetBasketClientAsync();

            if (basket == null)
                HandleNewBasket(item);
            else
                HandleExistingBasket(basket, item);

            if (!IsOperationValid()) return CustomResponse();

            await CommitAsync();         
            
            return CustomResponse();
        }

        [HttpPut("basket/{productId}")]
        public async Task<IActionResult> Put(Guid productId, BasketItem item)
        {
            var basket = await GetBasketClientAsync();
            var basketItem = await GetBasketItemValidatedAsync(productId, basket, item);
            if (basketItem == null) return CustomResponse();

            basket.UpdateUnits(basketItem, item.Quantity);

            ValidateBasket(basket);
            if (!IsOperationValid()) return CustomResponse();

            _context.BasketItems.Update(basketItem);
            _context.BasketClients.Update(basket);

            await CommitAsync();
            return CustomResponse();
        }

        [HttpDelete("basket/{productId}")]
        public async Task<IActionResult> Delete(Guid productId)
        {            
            var basket = await GetBasketClientAsync();

            var basketItem = await GetBasketItemValidatedAsync(productId, basket);
            if (basketItem == null) return CustomResponse();

            ValidateBasket(basket);
            if (!IsOperationValid()) return CustomResponse();

            basket.RemoveItem(basketItem);

            _context.BasketItems.Remove(basketItem);
            _context.BasketClients.Update(basket);

            await CommitAsync();
            return CustomResponse();
        }

        private async Task<BasketClient> GetBasketClientAsync()
        {
            return await _context.BasketClients
                .Include(x => x.Items)                
                .FirstOrDefaultAsync(c => c.ClientId == _user.GetId());
        }

        private void HandleNewBasket(BasketItem item)
        {
            var basket = new BasketClient(_user.GetId());
            basket.AddItem(item);

            _context.BasketClients.Add(basket);
        }
        private void HandleExistingBasket(BasketClient basket, BasketItem item)
        {
            var existingProductItem = basket.BasketItemExists(item);

            basket.AddItem(item);
            ValidateBasket(basket);

            if (existingProductItem)            
                _context.BasketItems.Update(basket.GetItemByProductId(item.ProductId));            
            else
                _context.BasketItems.Add(item);            

            _context.BasketClients.Update(basket);
        }
        private async Task<BasketItem> GetBasketItemValidatedAsync(Guid produtoId, BasketClient basket, BasketItem item = null)
        {
            if (item != null && produtoId != item.ProductId)
            {
                AddProcessingError("The item does not match the description");
                return null;
            }

            if (basket == null)
            {
                AddProcessingError("Basket not found");
                return null;
            }

            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(i => i.BasketId == basket.Id && i.ProductId == produtoId);
            if (basketItem == null || !basket.BasketItemExists(basketItem))
            {
                AddProcessingError("The item is not in the basket");
                return null;
            }

            return basketItem;
        }
        private async Task CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddProcessingError("It was not possible to save the data");
        }

        private void ValidateBasket(BasketClient basket)
        {
            if (basket.IsValid()) return;

            basket.ValidationResult.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
        }
    }
}
