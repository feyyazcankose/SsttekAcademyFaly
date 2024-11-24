using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos;
using Faly.Core.Dtos.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[ApiController]
[Route("api/admin/accounts")]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
public class AdminAccountController : CustomControllerBase
{
    private readonly IAdminAccountService _adminAccountService;

    public AdminAccountController(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Admin Login",
        Description = "Admin logs in and retrieves a JWT token."
    )]
    public async Task<IActionResult> Login([FromBody] AdminLoginDto loginDto)
    {
        return HandleServiceResult(await _adminAccountService.LoginAsync(loginDto));
    }
}
