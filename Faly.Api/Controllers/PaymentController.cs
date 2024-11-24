using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos.Ecommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/payments")]
[ApiExplorerSettings(GroupName = "Ecommerce > Payment")]
public class PaymentController : CustomControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Process Payment", Description = "Process a payment for an order.")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
    {
        return HandleServiceResult(await _paymentService.ProcessPaymentAsync(paymentDto));
    }
}
