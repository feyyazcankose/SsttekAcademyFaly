using System.Security.Claims;
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

    [HttpGet("profile")]
    [SwaggerOperation(
        Summary = "Get User Profile",
        Description = "Retrieve user profile from JWT."
    )]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine($"JWT'den alınan User ID: {userId}");

        if (string.IsNullOrEmpty(userId))
        {
            Console.WriteLine("User ID bulunamadı.");
            return Unauthorized("User ID not found in token.");
        }

        var user = await _userService.GetUserProfileAsync(userId);
        return HandleServiceResult(user);
    }

    [HttpPut("profile")]
    [SwaggerOperation(
        Summary = "Update User Profile",
        Description = "Update the authenticated user's profile."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateUserProfile([FromBody] ProfileUpdateDto profileUpdateDto)
    {
        Console.WriteLine("Profile Update");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        var result = await _userService.UpdateUserProfileAsync(userId, profileUpdateDto);
        return HandleServiceResult(result);
    }
}
