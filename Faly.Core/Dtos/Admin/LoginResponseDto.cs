namespace Faly.Core.Dtos.Admin;

public class LoginResponseDto
{
    public string Token { get; set; } = default!;
    public string Role { get; set; } = default!;
}
