using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class AdminUserDto
{
    [SwaggerSchema("Unique identifier of the user.")]
    [DefaultValue("abc-123")]
    public string Id { get; set; } = default!;

    [SwaggerSchema("Full name of the user.")]
    [DefaultValue("John Doe")]
    public string FullName { get; set; } = default!;

    [SwaggerSchema("Email address of the user.")]
    [DefaultValue("john.doe@example.com")]
    public string Email { get; set; } = default!;

    [SwaggerSchema("Role of the user.")]
    [DefaultValue("Admin")]
    public string Role { get; set; } = default!;
}