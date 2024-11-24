using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos;
using Faly.Core.Dtos.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/user-orders")]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
public class AdminUserOrderCourseController : CustomControllerBase
{
    private readonly IAdminUserOrderCourseService _adminUserOrderCourseService;

    public AdminUserOrderCourseController(IAdminUserOrderCourseService adminUserOrderCourseService)
    {
        _adminUserOrderCourseService = adminUserOrderCourseService;
    }

    [HttpGet("{orderId}/courses")]
    [ProducesResponseType(typeof(List<AdminCourseDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get Courses by Order",
        Description = "Retrieve courses for a specific order."
    )]
    public async Task<IActionResult> GetCoursesByOrder(int orderId)
    {
        return HandleServiceResult(
            await _adminUserOrderCourseService.GetCoursesByOrderAsync(orderId)
        );
    }
}
