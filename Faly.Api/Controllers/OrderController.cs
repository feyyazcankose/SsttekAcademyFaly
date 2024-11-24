using System.Security.Claims;
using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos.Ecommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/orders")]
[ApiExplorerSettings(GroupName = "Ecommerce > Order")]
public class OrderController : CustomControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
    {
        _orderService = orderService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Create Order", Description = "Create a new order.")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        var userId = _httpContextAccessor
            .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;
        return HandleServiceResult(await _orderService.CreateOrderAsync(userId, createOrderDto));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get User Orders",
        Description = "Retrieve orders of the logged-in user."
    )]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserOrders()
    {
        var userId = _httpContextAccessor
            .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;
        return HandleServiceResult(await _orderService.GetUserOrdersAsync(userId));
    }
}
