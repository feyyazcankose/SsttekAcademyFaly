using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class UserDto
{
    [SwaggerSchema("Unique identifier of the user.")]
    [DefaultValue("123e4567-e89b-12d3-a456-426614174000")]
    public string Id { get; set; } = default!;

    [SwaggerSchema("First name of the user.")]
    [DefaultValue("John")]
    public string FirstName { get; set; } = default!;

    [SwaggerSchema("Last name of the user.")]
    [DefaultValue("Doe")]
    public string LastName { get; set; } = default!;

    [SwaggerSchema("Phone Number of the user.")]
    [DefaultValue("5421413213231")]
    public string PhoneNumber { get; set; } = default!;

    [SwaggerSchema("Email address of the user.")]
    [DefaultValue("john.doe@example.com")]
    public string Email { get; set; } = default!;
}
