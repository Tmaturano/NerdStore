using Microsoft.AspNetCore.Mvc;
using NS.Orders.API.Application.DTO;
using NS.Orders.API.Application.Queries;
using NS.WebApi.Core.Controllers;
using System.Net;

namespace NS.Orders.API.Controllers;

[Route("api/voucher")]
[ApiController]
public class VoucherController : MainController
{
    private readonly IVoucherQueries _voucherQueries;

    public VoucherController(IVoucherQueries voucherQueries) => _voucherQueries = voucherQueries;

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(string code)
    {
        if (string.IsNullOrEmpty(code)) return NotFound();

        var voucher = await _voucherQueries.GetVoucherByCodeAsync(code);

        return voucher is null ? NotFound() : CustomResponse(voucher);
    }
}
