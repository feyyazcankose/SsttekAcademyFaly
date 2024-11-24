using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos.Ecommerce;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[ApiController]
[Route("api/users")]
[ApiExplorerSettings(GroupName = "Ecommerce > User Auth")]
public class UserController : CustomControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "User Registration", Description = "Register a new user.")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        return HandleServiceResult(await _userService.RegisterUserAsync(registrationDto));
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "User Login", Description = "Login an existing user.")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        return HandleServiceResult(await _userService.LoginUserAsync(loginDto));
    }
}
