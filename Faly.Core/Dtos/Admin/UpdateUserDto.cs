using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class UpdateUserDto
{
    [Required(ErrorMessage = "User ID is required")]
    [SwaggerSchema("Unique identifier of the user.")]
    [DefaultValue("abc-123")]
    public int Id { get; set; } = default!;

    [Required(ErrorMessage = "First name is required")]
    [SwaggerSchema("First name of the user.")]
    [DefaultValue("John")]
    public string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "Last name is required")]
    [SwaggerSchema("Last name of the user.")]
    [DefaultValue("Doe")]
    public string LastName { get; set; } = default!;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    [SwaggerSchema("Email address of the user.")]
    [DefaultValue("john.doe@example.com")]
    public string Email { get; set; } = default!;
}