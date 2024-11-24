using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Ecommerce;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Faly.BussinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<ServiceResult<AuthResponseDto>> RegisterUserAsync(
        UserRegistrationDto registrationDto
    )
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(registrationDto.Email);
        if (existingUser != null)
        {
            return ServiceResult<AuthResponseDto>.ErrorResult("Email is already in use.");
        }

        var newUser = new ApplicationUser
        {
            FirstName = registrationDto.FirstName,
            LastName = registrationDto.LastName,
            Email = registrationDto.Email,
            UserName = registrationDto.Email,
        };

        var result = await _userRepository.CreateUserAsync(newUser, registrationDto.Password);
        if (!result.Succeeded)
        {
            return ServiceResult<AuthResponseDto>.ErrorResult("User registration failed.");
        }

        var userDto = new UserDto
        {
            Id = newUser.Id,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
        };

        var token = GenerateJwtToken(newUser);
        var response = new AuthResponseDto { AccessToken = token };

        return ServiceResult<AuthResponseDto>.SuccessResult(
            response,
            "User registered successfully."
        );
    }

    public async Task<ServiceResult<AuthResponseDto>> LoginUserAsync(UserLoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return ServiceResult<AuthResponseDto>.ErrorResult("Invalid email or password.");
        }

        var isValidPassword = await _userRepository.CheckPasswordAsync(user, loginDto.Password);
        if (!isValidPassword)
        {
            return ServiceResult<AuthResponseDto>.ErrorResult("Invalid email or password.");
        }

        var token = GenerateJwtToken(user);
        var response = new AuthResponseDto { AccessToken = token };
        return ServiceResult<AuthResponseDto>.SuccessResult(response, "Login successful.");
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

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
