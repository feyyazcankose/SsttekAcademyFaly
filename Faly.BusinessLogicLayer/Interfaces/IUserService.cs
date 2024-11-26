using Faly.Core;
using Faly.Core.Dtos.Ecommerce;

namespace Faly.BussinessLogicLayer.Interfaces;

public interface IUserService
{
    Task<ServiceResult<AuthResponseDto>> RegisterUserAsync(UserRegistrationDto registrationDto);
    Task<ServiceResult<AuthResponseDto>> LoginUserAsync(UserLoginDto loginDto);

    Task<ServiceResult<UserDto>> GetUserProfileAsync(string userId);

    Task<ServiceResult> UpdateUserProfileAsync(string userId, ProfileUpdateDto profileUpdateDto);
}
