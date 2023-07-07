﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.BFF.Shopping.Models;
using NS.BFF.Shopping.Services;
using NS.WebApi.Core.Controllers;
using NSE.BFF.Shopping.Services;

namespace NS.BFF.Shopping.Controllers;

[Route("api/shopping")]
[Authorize]
public class BasketController : MainController
{
    private readonly IBasketService _basketService;
    private readonly ICatalogService _catalogService;
    private readonly IOrderService _orderService;

    public BasketController(IBasketService basketService, ICatalogService catalogService, IOrderService orderService)
    {
        _basketService = basketService;
        _catalogService = catalogService;
        _orderService = orderService;
    }

    [HttpGet("basket")]
    public async Task<IActionResult> Index() => CustomResponse(await _basketService.GetBasketAsync());

    [HttpGet("basket/count")]        
    public async Task<int> GetBasketCount()
    {
        var itemsCount = await _basketService.GetBasketAsync();
        return itemsCount?.Items.Sum(i => i.Quantity) ?? 0;
    }

    [HttpPost("basket/items")]        
    public async Task<IActionResult> AddBasketItem([FromBody]BasketItemDTO productItem)
    {
        var product = await _catalogService.GetById(productItem.ProductId);

        await ValidateItemBasket(product, productItem.Quantity);
        if (!IsOperationValid()) return CustomResponse();

        productItem.Name = product.Name;
        productItem.Price = product.Price;
        productItem.Image = product.Image;

        var response = await _basketService.AddItemBasketAsync(productItem);

        return CustomResponse(response);
    }

    [HttpPut("basket/items/{productId}")]        
    public async Task<IActionResult> UpdateBasketItem(Guid productId, [FromBody]BasketItemDTO productItem)
    {
        var product = await _catalogService.GetById(productItem.ProductId);

        await ValidateItemBasket(product, productItem.Quantity);
        if (!IsOperationValid()) return CustomResponse();

        var response = await _basketService.UpdateItemBasketAsync(productId, productItem);

        return CustomResponse(response);
    }

    [HttpDelete("basket/items/{productId}")]        
    public async Task<IActionResult> RemoveBasketItem(Guid productId)
    {
        var product = await _catalogService.GetById(productId);
        if (product is null)
        {
            AddProcessingError("Product not found");
            return CustomResponse();
        }

        
        var response = await _basketService.RemoveItemBasketAsync(productId);

        return CustomResponse(response);
    }

    [HttpPost("basket/apply-voucher")]    
    public async Task<IActionResult> ApplyVoucher([FromBody] string voucherCode)
    {
        var voucher = await _orderService.GetVoucherByCodeAsync(voucherCode);
        if (voucher is null)
        {
            AddProcessingError("Voucher inválido ou não encontrado!");
            return CustomResponse();
        }

        var resposta = await _basketService.ApplyVoucherBasketAsync(voucher);

        return CustomResponse(resposta);
    }

    private async Task ValidateItemBasket(ProductItemDTO product, int quantity)
    {
        if (product is null) AddProcessingError("Product not found!");
        if (quantity < 1) AddProcessingError($"Select at least one unit of the product {product.Name}");

        var basket = await _basketService.GetBasketAsync();
        var basketItem = basket.Items.FirstOrDefault(b => b.ProductId == product.Id);

        if (basketItem is not null && basketItem.Quantity + quantity > product.StockQuantity)
        {
            AddProcessingError($"The product {product.Name} has {product.StockQuantity} units in stock, but you selected {quantity}");
            return;
        }

        if (quantity > product?.StockQuantity) AddProcessingError($"The product {product.Name} has {product.StockQuantity} units in stock, you selected {quantity}");
    }
}
