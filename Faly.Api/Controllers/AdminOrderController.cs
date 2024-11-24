using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos;
using Faly.Core.Dtos.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/orders")]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
[ApiExplorerSettings(GroupName = "Admin > Order")]
public class AdminOrderController : CustomControllerBase
{
    private readonly IAdminOrderService _adminOrderService;

    public AdminOrderController(IAdminOrderService adminOrderService)
    {
        _adminOrderService = adminOrderService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AdminOrderDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Get All Orders", Description = "Retrieve a list of all orders.")]
    public async Task<IActionResult> GetAllOrders()
    {
        return HandleServiceResult(await _adminOrderService.GetAllOrdersAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AdminOrderDto), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get Order Details",
        Description = "Retrieve details about a specific order."
    )]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        return HandleServiceResult(await _adminOrderService.GetOrderByIdAsync(id));
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(List<AdminOrderDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get User Orders",
        Description = "Retrieve all orders for a specific user."
    )]
    public async Task<IActionResult> GetUserOrders(string userId)
    {
        return HandleServiceResult(await _adminOrderService.GetOrdersByUserIdAsync(userId));
    }
}
