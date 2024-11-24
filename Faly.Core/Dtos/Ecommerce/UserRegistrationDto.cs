using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class UserRegistrationDto
{
    [Required(ErrorMessage = "First name is required.")]
    [SwaggerSchema("First name of the user.")]
    [DefaultValue("John")]
    public string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "Last name is required.")]
    [SwaggerSchema("Last name of the user.")]
    [DefaultValue("Doe")]
    public string LastName { get; set; } = default!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [SwaggerSchema("Email address of the user.")]
    [DefaultValue("john.doe@example.com")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required.")]
    [SwaggerSchema("Password for the user account.")]
    [DefaultValue("P@ssw0rd!")]
    public string Password { get; set; } = default!;
}
