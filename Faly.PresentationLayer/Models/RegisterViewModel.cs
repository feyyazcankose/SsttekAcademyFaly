using System.ComponentModel.DataAnnotations;

namespace Faly.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Ad gereklidir.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad gereklidir.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "E-posta adresi gereklidir.")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre gereklidir.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Şifre tekrarı gereklidir.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; set; }
}
