using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Faly.Core.Dtos.Admin;

public class AdminLoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [DefaultValue("dev@ssttek.com")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [DefaultValue("Ssttek123")]
    public string Password { get; set; } = default!;
}
