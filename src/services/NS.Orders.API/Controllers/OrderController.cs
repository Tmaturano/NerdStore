using Microsoft.AspNetCore.Mvc;
using NS.Core.Mediator;
using NS.Orders.API.Application.Commands;
using NS.Orders.API.Application.Queries;
using NS.WebApi.Core.Controllers;
using NS.WebApi.Core.User;

namespace NS.Orders.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : MainController
{
    private readonly IMediatorHandler _mediator;
    private readonly IAspNetUser _user;
    private readonly IOrderQueries _orderQueries;

    public OrderController(IMediatorHandler mediator, IAspNetUser user, IOrderQueries orderQueries)
    {
        _mediator = mediator;
        _user = user;
        _orderQueries = orderQueries;
    }

    [HttpPost()]
    public async Task<IActionResult> Post(AddOrderCommand order)
    {
        order.ClientId = _user.GetId();
        return CustomResponse(await _mediator.SendCommand(order));
    }

    [HttpGet("last")]
    public async Task<IActionResult> GetLastOrder()
    {
        var pedido = await _orderQueries.GetLastOrderAsync(_user.GetId());

        return pedido is null ? NotFound() : CustomResponse(pedido);
    }

    [HttpGet("client-list")]
    public async Task<IActionResult> GetListByClient()
    {
        var pedidos = await _orderQueries.GetListByClientIdAsync(_user.GetId());

        return pedidos is null ? NotFound() : CustomResponse(pedidos);
    }
}
