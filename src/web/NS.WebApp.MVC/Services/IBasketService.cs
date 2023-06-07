using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services;

public interface IBasketService
{
    Task<BasketViewModel> GetBasketAsync();
    Task<ResponseResult> AddItemBasketAsync(ProductItemViewModel product);
    Task<ResponseResult> UpdateItemBasketAsync(Guid productId, ProductItemViewModel product);
    Task<ResponseResult> RemoveItemBasketAsync(Guid productId);
}
