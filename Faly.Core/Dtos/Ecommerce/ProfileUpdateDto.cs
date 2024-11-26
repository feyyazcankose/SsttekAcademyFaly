using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Faly.Core.Dtos.Ecommerce;

public class ProfileUpdateDto
{
    [Required(ErrorMessage = "First name is required.")]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "Last name is required.")]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = default!;

    [Required(ErrorMessage = "Email is required.")]
    [DisplayName("Email")]
    public string Email { get; set; } = default!;

    [Phone(ErrorMessage = "Invalid phone number.")]
    [DisplayName("Phone Number")]
    public string? PhoneNumber { get; set; }
}
