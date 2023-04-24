using Microsoft.AspNetCore.Mvc;
using NS.Clients.API.Application.Commands;
using NS.Core.Mediator;
using NS.WebApi.Core.Controllers;

namespace NS.Clients.API.Controllers
{
    [Route("api/client")]
    public class ClientsController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientsController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(new AddClientCommand(Guid.NewGuid(), "Thiago", "thiago@teste.com", "98467989033"));

            return CustomResponse(result);
        }
    }
}
