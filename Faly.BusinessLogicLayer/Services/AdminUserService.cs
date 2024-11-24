using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.BussinessLogicLayer.Services;

public class AdminUserService : IAdminUserService
{
    private readonly IAdminRepository<ApplicationUser> _userRepository;

    public AdminUserService(IAdminRepository<ApplicationUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<IEnumerable<AdminUserDto>>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var userDtos = users.Select(user => new AdminUserDto
        {
            Id = user.Id,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Role = "User" // Eğer role yönetimi varsa, burada ilgili rolü alabilirsiniz.
        });

        return ServiceResult<IEnumerable<AdminUserDto>>.SuccessResult(userDtos, "Users retrieved successfully.");
    }

    public async Task<ServiceResult<AdminUserDto>> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return ServiceResult<AdminUserDto>.ErrorResult("User not found.");

        var userDto = new AdminUserDto
        {
            Id = user.Id,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Role = "User"
        };

        return ServiceResult<AdminUserDto>.SuccessResult(userDto, "User retrieved successfully.");
    }

    public async Task<ServiceResult> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new ApplicationUser
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email,
            PasswordHash = createUserDto.Password // Password hashing işlemi yapılmalı
        };

        await _userRepository.AddAsync(user);
        return ServiceResult.SuccessResult(201, "User created successfully.");
    }

    public async Task<ServiceResult> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(updateUserDto.Id);
        if (user == null)
            return ServiceResult.ErrorResult("User not found.");

        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.Email = updateUserDto.Email;

        await _userRepository.UpdateAsync(user);
        return ServiceResult.SuccessResult(200, "User updated successfully.");
    }

    public async Task<ServiceResult> DeleteUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return ServiceResult.ErrorResult("User not found.");

        await _userRepository.DeleteAsync(user);
        return ServiceResult.SuccessResult(200, "User deleted successfully.");
    }
}

