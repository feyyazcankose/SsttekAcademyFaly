using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Faly.BussinessLogicLayer.Services;

public class AdminAccountService : IAdminAccountService
{
    private readonly IAdminAccountRepository _accountRepository;
    private readonly IConfiguration _configuration;

    public AdminAccountService(
        IAdminAccountRepository accountRepository,
        IConfiguration configuration
    )
    {
        _accountRepository = accountRepository;
        _configuration = configuration;
    }

    public async Task<ServiceResult<LoginResponseDto>> LoginAsync(AdminLoginDto loginDto)
    {
        var isValid = await _accountRepository.ValidateCredentialsAsync(
            loginDto.Email,
            loginDto.Password
        );
        if (!isValid)
        {
            return ServiceResult<LoginResponseDto>.ErrorResult("Invalid email or password.");
        }

        var role = await _accountRepository.GetRoleAsync(loginDto.Email);
        var token = GenerateJwtToken(loginDto.Email, role);

        return ServiceResult<LoginResponseDto>.SuccessResult(
            new LoginResponseDto { Token = token, Role = role },
            "Login successful."
        );
    }

    private string GenerateJwtToken(string email, string role)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[] { new Claim(ClaimTypes.Email, email), new Claim(ClaimTypes.Role, role) };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["Expires"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
