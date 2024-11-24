using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos;
using Faly.Core.Dtos.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/users")]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
public class AdminUserController : CustomControllerBase
{
    private readonly IAdminUserService _adminUserService;

    public AdminUserController(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AdminUserDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Get All Users", Description = "Retrieve a list of all users.")]
    public async Task<IActionResult> GetAllUsers()
    {
        return HandleServiceResult(await _adminUserService.GetAllUsersAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AdminUserDto), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get User Details",
        Description = "Retrieve detailed information about a specific user."
    )]
    public async Task<IActionResult> GetUserDetails(int id)
    {
        return HandleServiceResult(await _adminUserService.GetUserByIdAsync(id));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create User", Description = "Create a new user.")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        return HandleServiceResult(await _adminUserService.CreateUserAsync(createUserDto));
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Update User",
        Description = "Update information for an existing user."
    )]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        return HandleServiceResult(await _adminUserService.UpdateUserAsync(updateUserDto));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete User", Description = "Delete a user by their ID.")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        return HandleServiceResult(await _adminUserService.DeleteUserAsync(id));
    }
}
